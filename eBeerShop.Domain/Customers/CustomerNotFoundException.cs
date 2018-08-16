using System;

namespace eBeerShop.Domain.Customers
{
    public class CustomerNotFoundException: Exception
    {
        public CustomerNotFoundException() 
            : base()
        { }
    }
}
