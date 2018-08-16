using System;
using System.Collections.Generic;
using System.Text;

namespace eBeerShop.Domain.Customers
{
    public interface ICustomerRepository
    {
        Customer RetrieveCustomer(int v);
    }
}
