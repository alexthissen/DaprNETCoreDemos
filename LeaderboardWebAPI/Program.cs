using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr.Actors.AspNetCore;
using Dapr.Client;
using Dapr.Extensions.Configuration;
using LeaderboardWebAPI.Actors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LeaderboardWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((ctx, builder) =>
                {
                    // This approach is still under discussion. 
                    // DaprClient is not available until after app has started.

                    //var client = new DaprClientBuilder().Build();
                    //builder.AddDaprSecretStore(config => {
                    //    config.Store = "azurekeyvault";
                    //    config.Client = client;
                    //    config.SecretDescriptors =
                    //        new [] { new DaprSecretDescriptor("mysecret") };
                    //});
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>()
                        .UseActors(actorRuntime =>
                        {
                            actorRuntime.ConfigureActorSettings(a =>
                            {
                                a.ActorIdleTimeout = TimeSpan.FromMinutes(70);
                                a.ActorScanInterval = TimeSpan.FromSeconds(35);
                                a.DrainOngoingCallTimeout = TimeSpan.FromSeconds(35);
                                a.DrainRebalancedActors = true;
                            });
                            actorRuntime.RegisterActor<LeaderboardActor>();
                        });
                });
    }
}
