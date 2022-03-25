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
    [BondTypeName(BondType.NTNB)]
    public class BondNTNBService : BaseService, IBondService
    {
        private readonly string ntnbUrl = "tesouro/tesouro-ipca-com-juros-semestrais-";
        private readonly ILogger<BondNTNBService> logger;

        public BondNTNBService(ILogger<BondNTNBService> logger)
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
            logger.LogInformation($"BondNTNBService - querying data");
            var bondTreasury = payload.Response?.TrsrBdTradgList
                .Where(bond => bond.TrsrBd.Nm.Contains(year) && bond.TrsrBd.Nm.ToString().Contains("Juros Semestrais") && bond.TrsrBd.FinIndxs.Nm.ToString().ToUpper().Equals("IPCA"))
                .FirstOrDefault();
            logger.LogInformation($"Page title: {bondTreasury?.TrsrBd.Nm} / Bond sale value: {bondTreasury?.TrsrBd.SellValue} / Bond buy value: {bondTreasury?.TrsrBd.BuyValue}");
            return TreasuryBond.Create(bondTreasury?.TrsrBd.Nm, (decimal)bondTreasury?.TrsrBd.SellValue, (decimal)bondTreasury?.TrsrBd.BuyValue);
        }
    }
}
