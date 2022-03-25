using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TreasuryBondPrice.Core.Model;

namespace TreasuryBondPrice.Core.Service.Interface
{
    public interface IBondService
    {
        Task<TreasuryBond> GetTreasury(string year);
    }
}
