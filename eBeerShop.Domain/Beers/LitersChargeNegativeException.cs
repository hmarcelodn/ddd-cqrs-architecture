using System;
using System.Collections.Generic;
using System.Text;

namespace eBeerShop.Domain.Beers
{
    public class LitersChargeNegativeException 
        : Exception
    {
        public LitersChargeNegativeException()
            : base("The pricing is to low")
        { }
    }
}
