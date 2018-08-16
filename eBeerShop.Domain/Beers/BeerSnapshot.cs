namespace eBeerShop.Domain.Beers
{
    public class BeerSnapshot
    {
        protected BeerSnapshot()
        { }

        public BeerSnapshot(string description, decimal literPrice, int id)
        {
            Id = id;
            Description = description;
            LiterPrice = literPrice;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }
        public decimal LiterPrice { get; private set; }
    }
}
