using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TreasuryBondPrice.Core.Service.Interface;
using TreasuryBondPrice.Core.Model;
using TreasuryBondPrice.Core.Handler.Interface;

namespace TreasuryBondPrice
{
    public class TreasuryPriceFunction
    {
        private readonly IRequestProcessor requestProcessor;
        public TreasuryPriceFunction(IRequestProcessor requestProcessor)
        {
            this.requestProcessor = requestProcessor;
        }

        [FunctionName("treasuryPrice")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string year = req.Query["year"];
            string type = req.Query["type"];

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(year)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string."
                : JsonConvert.SerializeObject(await requestProcessor.Process(type, year));

            return new OkObjectResult(responseMessage);
        }
    }
}
