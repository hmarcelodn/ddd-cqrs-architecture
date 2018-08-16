using eBeerShop.Domain.Shared;
using System;

namespace eBeerShop.Domain.Customers
{
    public class Customer : EntityBase, IAggregateRoot
    {
        public bool CanBeSaved => throw new NotImplementedException();

        protected Customer()
        { }

        public Customer(Address address, string firstName, string lastName, string email) 
            : this()
        {
            if (address is null) throw new ArgumentNullException("address");
            if (string.IsNullOrEmpty(firstName)) throw new ArgumentNullException("firstName");
            if (string.IsNullOrEmpty(lastName)) throw new ArgumentNullException("lastName");
            if (string.IsNullOrEmpty(email)) throw new ArgumentNullException("email");

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
        }

        public string FirstName { get; private set; }
        
        public string LastName { get; private set; }

        public string Email { get; private set; }

        public Address Address { get; private set; }

        public void SetFirstName(string firstName)
        {
            if (string.IsNullOrEmpty(firstName)) throw new ArgumentNullException("firstName");

            this.FirstName = firstName;
        }

        public void SetLastName(string lastName)
        {
            if (string.IsNullOrEmpty(lastName)) throw new ArgumentNullException("lastName");

            this.LastName = lastName;
        }

        public void SetEmailAddress(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress)) throw new ArgumentNullException("emailAddress");

            this.Email = emailAddress;
        }

        public void SetAddress(Address address)
        {
            if (address is null) throw new ArgumentNullException("address");

            Address = address;
        }

        public CustomerSnapshot TakeSnapshot()
        {
            return new CustomerSnapshot(Email, FirstName, LastName, Id);
        }
    }
}
