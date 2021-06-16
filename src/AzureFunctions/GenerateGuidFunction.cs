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
    public class GenerateGuidFunction
    {
        private readonly ILogger _logger;

        public GenerateGuidFunction(ILogger<GenerateGuidFunction> logger)
        {
            _logger = logger;
        }

        [FunctionName(nameof(GenerateGuidFunction))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "identifiers")] 
            HttpRequest req)
        {
            _logger.LogInformation("Executing {functionName}, trying to authenticate the request...", nameof(GenerateGuidFunction));
            var (authenticationStatus, authenticationResponse) = await req.HttpContext.AuthenticateFunctionAsync("Bearer");
            if (!authenticationStatus)
            {
                _logger.LogInformation("Authentication failed with response {authenticationResponse}", authenticationResponse);
                return authenticationResponse;
            }

            _logger.LogInformation("Authentication Ok, generating a new guid...");
            return new OkObjectResult(Guid.NewGuid());
        }
    }
}
