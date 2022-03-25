using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TreasuryBondPrice.Core.Model;
using TreasuryBondPrice.Core.Service.Interface;
using TreasuryBondPrice.Core.Util;

namespace TreasuryBondPrice.Core.Handler.Interface
{
    public class RequestProcessor : IRequestProcessor
    {
        private readonly IEnumerable<IBondService> services;
        public RequestProcessor(IEnumerable<IBondService> services)
        {
            this.services = services;
        }
        public async Task<TreasuryBond> Process(string bondType, string year)
        {
            var service = services.Where(s => s.GetType()
                            .GetCustomAttribute<BondTypeName>().Name.Equals(bondType))
                            .FirstOrDefault() ??
                          services.Where(s => s.GetType()
                            .GetCustomAttribute<BondTypeName>().Name.Equals(BondType.DEFAULT)).Single();

            return await service?.GetTreasury(year);
        }
    }
}
