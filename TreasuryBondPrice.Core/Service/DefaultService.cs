using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TreasuryBondPrice.Core.Model;
using TreasuryBondPrice.Core.Service.Interface;
using TreasuryBondPrice.Core.Util;

namespace TreasuryBondPrice.Core.Service
{
    [BondTypeName(BondType.DEFAULT)]
    public class DefaultService : IBondService
    {
        public async Task<TreasuryBond> GetTreasury(string year)
        {
            return await Task.FromResult(new Invalid());
        }
    }
}
