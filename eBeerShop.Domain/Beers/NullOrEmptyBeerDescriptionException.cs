using System;
using System.Collections.Generic;
using System.Text;

namespace eBeerShop.Domain.Beers
{
    public class NullOrEmptyBeerDescriptionException : Exception
    {
        public NullOrEmptyBeerDescriptionException() 
            : base("Description cannot be empty")
        { }
    }
}
