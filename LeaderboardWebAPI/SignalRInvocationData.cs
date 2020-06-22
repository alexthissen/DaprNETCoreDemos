using System.Collections.Generic;
using System.Collections.Specialized;

namespace LeaderboardWebAPI
{
    public class Argument
    {
        public string name { get; set; }
        public string message { get; set; }
    }

    public class SignalRInvocationData
    {
        public string Target { get; set; }
        public List<string> Arguments { get; set; }
    }
}