using System;

namespace eBeerShop.Domain.Beers
{
    public class LitersToRemoveExcedeedException: Exception
    {
        public LitersToRemoveExcedeedException() 
            : base("Liters to remove exceeded the available stock")
        { }
    }
}
