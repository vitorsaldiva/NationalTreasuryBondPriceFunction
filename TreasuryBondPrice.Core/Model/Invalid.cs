using System;
using System.Collections.Generic;
using System.Text;

namespace TreasuryBondPrice.Core.Model
{
    public class Invalid : TreasuryBond
    {
        internal Invalid() : base("Invalid title", default, default) { }
    }
}
