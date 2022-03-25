using System;
using System.Collections.Generic;
using System.Text;

namespace TreasuryBondPrice.Core.Util
{
    [AttributeUsage(AttributeTargets.Class)]
    public class BondTypeName : Attribute
    {
        public string Name { get; }
        public BondTypeName(string name)
        {
            this.Name = name;
        }
    }
}
