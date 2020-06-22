using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr;
using Dapr.Actors;
using Dapr.Actors.Client;
using Dapr.Client;
using LeaderboardWebAPI.Actors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace LeaderboardWebAPI.Controllers
{
    [ApiController]
    public class MostMentionedController : ControllerBase
    {
        private readonly ILogger<MostMentionedController> logger;
        private readonly IHubContext<LeaderboardHub> hubContext;
        public const string TweetStoreName = "statestore";
        public const string MentionStoreName = "statestore";

        public MostMentionedController(ILogger<MostMentionedController> logger, IHubContext<LeaderboardHub> hubContext)
        {
            this.logger = logger;
            this.hubContext = hubContext;
        }

        [Topic("mentions")]
        [HttpPost("mentions")]
        public async Task<ActionResult> HandleMentions(TweetReceived message, [FromServices] DaprClient daprClient)
        {
            logger.LogInformation($"Twitter message from @{message.Tweet.user.screen_name}");
            await daprClient.SaveStateAsync(TweetStoreName, message.Tweet.id_str, message.Tweet);

            ActorId actorId = new ActorId(message.Tag);
            var proxy = ActorProxy.Create<ILeaderboardActor>(actorId, "LeaderboardActor");

            var mentions = message.Tweet.entities.user_mentions;
            foreach (UserMention mention in mentions)
            {
                var entry = await daprClient.GetStateEntryAsync<UserMentionCount>(MentionStoreName, mention.id_str, ConsistencyMode.Eventual);
                entry.Value ??= new UserMentionCount { Count = 0, Mention = mention };
                entry.Value.Count++;
                await entry.SaveAsync();

                await proxy.SubmitScore(entry.Value.Mention.id_str, 
                    entry.Value.Mention.name, entry.Value.Mention.screen_name,
                    entry.Value.Count);
            }

            var ranking = await proxy.GetRanking(10);

            var data = new SignalRInvocationData()
            {
                Target = "leaderboardUpdated",
                Arguments = new List<string>()
            };
            await daprClient.InvokeBindingAsync<SignalRInvocationData>("signalr", "create", data);
            
            return Ok();
        }

        [HttpGet("tweet/{tweetid}")]
        public ActionResult<Tweet> Get([FromState(TweetStoreName)]StateEntry<Tweet> tweet)
        {
            if (tweet.Value is null)
            {
                return NotFound();
            }
            return tweet.Value;
        }
    }
}
