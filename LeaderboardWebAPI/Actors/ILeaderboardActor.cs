using Dapr.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderboardWebAPI.Actors
{
    public interface ILeaderboardActor: IActor
    {
        Task SubmitScore(string key, string name, string screenName, int score);
        Task<List<LeaderboardEntry>> GetRanking(int top);
    }
}
