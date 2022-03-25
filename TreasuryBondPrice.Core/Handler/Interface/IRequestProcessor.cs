using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TreasuryBondPrice.Core.Model;
using TreasuryBondPrice.Core.Service.Interface;

namespace TreasuryBondPrice.Core.Handler.Interface
{
    public interface IRequestProcessor
    {
        Task<TreasuryBond> Process(string bondType, string year);
    }
}
