using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasuryBondPrice.Core.Model;
using TreasuryBondPrice.Core.Service.Interface;
using TreasuryBondPrice.Core.Util;

namespace TreasuryBondPrice.Core.Service
{
    [BondTypeName(BondType.NTNF)]
    public class BondNTNFService : BaseService, IBondService
    {
        private readonly string ntnfUrl = "tesouro/tesouro-prefixado-com-juros-semestrais-";
        private readonly ILogger<BondNTNBService> logger;

        public BondNTNFService(ILogger<BondNTNBService> logger)
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
            logger.LogInformation($"BondNTNFService - querying data");
            var bondTreasury = payload.Response?.TrsrBdTradgList
                .Where(bond => bond.TrsrBd.Nm.Contains(year) && bond.TrsrBd.FinIndxs.Nm.ToString().ToUpper().Equals("PREFIXADO"))
                .FirstOrDefault();
            logger.LogInformation($"Page title: {bondTreasury?.TrsrBd.Nm} / Bond sale value: {bondTreasury?.TrsrBd.SellValue} / Bond buy value: {bondTreasury?.TrsrBd.BuyValue}");
            return TreasuryBond.Create(bondTreasury?.TrsrBd.Nm, (decimal)bondTreasury?.TrsrBd.SellValue, (decimal)bondTreasury?.TrsrBd.BuyValue);
        }
    }
}
