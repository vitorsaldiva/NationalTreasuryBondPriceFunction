using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TreasuryBondPrice.Core.Model;

namespace TreasuryBondPrice.Core.Repository
{
    public interface IRepository
    {
        Task<IEnumerable<TreasuryBond>> GetAllAsync();
        Task<IEnumerable<TreasuryBond>> GetByTypeAsync(string treasuryType);
        Task<IEnumerable<TreasuryBond>> GetByYearAsync(string year);
        Task SaveAsync(TreasuryBond treasuryBond);
    }
}
