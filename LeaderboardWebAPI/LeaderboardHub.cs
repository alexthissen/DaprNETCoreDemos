using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderboardWebAPI
{
    public class LeaderboardHub : Hub
    {
        public void LeaderboardUpdated()
        {
            Clients.All.SendAsync("leaderboardUpdated");
        }
    }
}
