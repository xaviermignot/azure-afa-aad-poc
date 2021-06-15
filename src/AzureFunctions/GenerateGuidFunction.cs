using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using AzureFunctionsWithAad.Extensions;

namespace AzureFunctionsWithAad.AzureFunctions
{
    public static class GenerateGuidFunction
    {
        [FunctionName("GenerateGuidFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "identifiers")] HttpRequest req,
            ILogger log)
        {
            var (authenticationStatus, authenticationResponse) = await req.HttpContext.AuthenticateFunctionAsync("Bearer");
            if (!authenticationStatus)
            {
                return authenticationResponse;
            }

            return new OkObjectResult(Guid.NewGuid());
        }
    }
}
