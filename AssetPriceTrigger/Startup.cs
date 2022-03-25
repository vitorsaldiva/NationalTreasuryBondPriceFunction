using AssetPriceTrigger.Repository;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using TreasuryBondPrice.Core.Handler.Interface;
using TreasuryBondPrice.Core.Repository;
using TreasuryBondPrice.Core.Service;
using TreasuryBondPrice.Core.Service.Interface;


[assembly: FunctionsStartup(typeof(AssetPriceTrigger.Startup))]
namespace AssetPriceTrigger
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IBondService, DefaultService>();
            builder.Services.AddSingleton<IBondService, BondLFTService>();
            builder.Services.AddSingleton<IBondService, BondNTNBService>();
            builder.Services.AddSingleton<IBondService, BondNTNBPrincService>();
            builder.Services.AddSingleton<IBondService, BondNTNFService>();
            builder.Services.AddSingleton<IRepository, TreasuryRepository>();
            builder.Services.AddSingleton<IRequestProcessor, RequestProcessor>();
            builder.Services.AddLogging();
        }
    }
}
