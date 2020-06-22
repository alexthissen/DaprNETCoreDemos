using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.Json;
using System.Threading.Tasks;
using Dapr;
using Dapr.Actors;
using Dapr.Actors.Client;
using Dapr.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LeaderboardWebAPI.Controllers
{
    [ApiController]
    public class SecretsController : ControllerBase
    {
        [HttpGet("secrets")]
        public async Task<ActionResult> GetSecret(string secretName, [FromServices] DaprClient daprClient)
        {
            var secretValues = await daprClient.GetSecretAsync(
                "azurekeyvault",
                "daprsecret");
            //, new Dictionary<string, string>() { { "namespace", "default" } }); // Namespace where Kubernetes secret is deployed

            // Get secret value. In Azure KeyVault secret only has one value with same key
            var secretValue = secretValues["daprsecret"];

            return new JsonResult(secretValue);
        }
    }
}
