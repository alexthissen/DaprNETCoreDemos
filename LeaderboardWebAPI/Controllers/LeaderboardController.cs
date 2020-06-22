using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Dapr;
using Dapr.Actors;
using Dapr.Actors.Client;
using Dapr.Client;
using LeaderboardWebAPI.Actors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LeaderboardWebAPI.Controllers
{
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        public const string StoreName = "statestore";

        [HttpGet("mostmentioned/{tag}")]
        public async Task<ActionResult<List<LeaderboardEntry>>> MostMentioned(string tag, [FromQuery] int top = 10)
        {
            ActorId actorId = new ActorId(tag);
            var proxy = ActorProxy.Create<ILeaderboardActor>(actorId, "LeaderboardActor");

            return await proxy.GetRanking(top);
        }
    }
}
