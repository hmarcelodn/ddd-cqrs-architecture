namespace eBeerShop.Domain.Customers
{
    public class Address
    {
        protected Address()
        { }

        public Address(string city, string street, string zip, string country) 
            : this()
        {
            City = city;
            Street = street;
            Zip = zip;
            Country = country;
        }

        public string City { get; private set; }

        public string Street { get; private set; }

        public string Zip { get; private set; }

        public string Country { get; private set; }
    }
}
