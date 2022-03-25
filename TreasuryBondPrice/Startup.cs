using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TreasuryBondPrice.Core.Handler.Interface;
using TreasuryBondPrice.Core.Service;
using TreasuryBondPrice.Core.Service.Interface;

[assembly: FunctionsStartup(typeof(TreasuryBondPrice.Startup))]
namespace TreasuryBondPrice
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
            builder.Services.AddSingleton<IRequestProcessor, RequestProcessor>();
            builder.Services.AddLogging();
        }
    }
}
