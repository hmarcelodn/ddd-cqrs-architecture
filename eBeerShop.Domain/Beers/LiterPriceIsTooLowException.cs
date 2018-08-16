using System;
using System.Collections.Generic;
using System.Text;

namespace eBeerShop.Domain.Beers
{
    public class LiterPriceIsTooLowException : Exception
    {
        public LiterPriceIsTooLowException() 
            : base("The liters price is too low")
        { }
    }
}
