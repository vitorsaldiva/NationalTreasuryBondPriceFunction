using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TreasuryBondPrice.Core.Model;
using TreasuryBondPrice.Core.Service.Interface;
using TreasuryBondPrice.Core.Util;

namespace TreasuryBondPrice.Core.Service
{
    [BondTypeName(BondType.NTNBPrinc)]
    public class BondNTNBPrincService : BaseService, IBondService
    {
        private readonly string ntnbPrincUrl = "tesouro/tesouro-ipca-";
        private readonly ILogger<BondNTNBPrincService> logger;

        public BondNTNBPrincService(ILogger<BondNTNBPrincService> logger)
        {
            this.logger = logger;
        }

        public async Task<TreasuryBond> GetTreasury(string year)
        {
            var payload = await GetTreasuries();
            var treasuryBond = GetTreasuryBond(payload, year);
            return treasuryBond;
        }

        protected override TreasuryBond GetTreasuryBond(NationalTreasury payload, string year)
        {
            logger.LogInformation($"BondNTNBPrincService - querying data");
            var bondTreasury = payload.Response?.TrsrBdTradgList
                .Where(bond => bond.TrsrBd.Nm.Contains(year) && bond.TrsrBd.FinIndxs.Nm.ToString().ToUpper().Equals("IPCA"))
                .FirstOrDefault();
            logger.LogInformation($"Page title: {bondTreasury?.TrsrBd.Nm} / Bond sale value: {bondTreasury?.TrsrBd.SellValue} / Bond buy value: {bondTreasury?.TrsrBd.BuyValue}");
            return TreasuryBond.Create(bondTreasury?.TrsrBd.Nm, (decimal)bondTreasury?.TrsrBd.SellValue, (decimal)bondTreasury?.TrsrBd.BuyValue);
        }
    }
}
