using System;

namespace eBeerShop.Domain.Beers
{
    public class LitersToRemoveIsZeroException : Exception
    {
        public LitersToRemoveIsZeroException() 
            : base("Liters to remove is zero")
        { }
    }
}
