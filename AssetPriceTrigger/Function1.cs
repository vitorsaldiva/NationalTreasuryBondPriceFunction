using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using TreasuryBondPrice.Core.Handler.Interface;
using TreasuryBondPrice.Core.Model;
using TreasuryBondPrice.Core.Repository;

namespace AssetPriceTrigger
{
    public class Function1
    {
        private readonly IRequestProcessor requestProcessor;
        private readonly IRepository treasuryRepository;

        public Function1(IRequestProcessor requestProcessor, IRepository treasuryRepository)
        {
            this.requestProcessor = requestProcessor;
            this.treasuryRepository = treasuryRepository;
        }

        [FunctionName("Function1")]
        public async Task Run([TimerTrigger("0 */60 12-20 * * MON-FRI")] TimerInfo myTimer, ILogger log)

        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            await treasuryRepository.SaveAsync(await requestProcessor.Process(BondType.LFT, "2024"));
            await treasuryRepository.SaveAsync(await requestProcessor.Process(BondType.NTNBPrinc, "2045"));
        }
    }
}
