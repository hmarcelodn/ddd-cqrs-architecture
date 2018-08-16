using System;
using System.Collections.Generic;
using System.Text;

namespace eBeerShop.Domain.Customers
{
    public class CustomerSnapshot
    {
        protected CustomerSnapshot()
        { }

        public CustomerSnapshot(string email, string firstName, string lastName, int id) 
            : this()
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Id = id;
        }

        public string Email { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public int Id { get; private set; }
    }
}
