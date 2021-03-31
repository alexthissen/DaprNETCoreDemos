using Dapr.Actors;
using Dapr.Actors.Runtime;
using Dapr.Client;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderboardWebAPI.Actors
{
    public class LeaderboardEntry
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ScreenName { get; set; }
        public int Score { get; set; }
    }

    [Actor(TypeName = "LeaderboardActor")]
    public class LeaderboardActor : Actor, ILeaderboardActor
    {
        private readonly int MaxEntries = 10;

        public LeaderboardActor(ActorHost host): base(host)
        {
        }

        public async Task<List<LeaderboardEntry>> GetRanking(int top)
        {
            var current = await this.StateManager.TryGetStateAsync<List<LeaderboardEntry>>("orderedlist");
            return current.HasValue ? current.Value.Take(top).ToList() : new List<LeaderboardEntry>();
        }

        public async Task SubmitScore(string id, string name, string screenName, int score)
        {
            var current = await this.StateManager.TryGetStateAsync<List<LeaderboardEntry>>("orderedlist");
            var list = current.HasValue ? current.Value : new List<LeaderboardEntry>();

            list.RemoveAll(entry => entry.Id == id);
            list.Add(new LeaderboardEntry() { Id = id, Name = name, ScreenName = screenName, Score = score });
            
            await this.StateManager.SetStateAsync("orderedlist", 
                list.OrderByDescending(entry => entry.Score).Take(MaxEntries).ToList());
        }
    }
}
