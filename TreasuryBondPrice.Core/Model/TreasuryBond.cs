using System;
using System.Collections.Generic;
using System.Text;

namespace TreasuryBondPrice.Core.Model
{
    public class TreasuryBond
    {
        public string Name { get; set; }
        public decimal SaleValue { get; set; }
        public decimal BuyValue { get; set; }

        protected TreasuryBond(string name, decimal saleValue, decimal buyValue)
        {
            Name = name;
            SaleValue = saleValue;
            BuyValue = buyValue;
        }

        public static TreasuryBond Create(string name, decimal saleValue, decimal buyValue)
        {
            if (string.IsNullOrEmpty(name) || saleValue < 0 || buyValue < 0)
                return new Invalid();
            return new TreasuryBond(name, saleValue, buyValue);
        }
    }
}
