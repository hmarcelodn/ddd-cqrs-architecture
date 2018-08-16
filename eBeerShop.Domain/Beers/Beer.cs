using eBeerShop.Domain.Shared;
using System;

namespace eBeerShop.Domain.Beers
{
    public class Beer: EntityBase, IAggregateRoot
    {
        public decimal LiterPrice { get; protected set; }

        public string Description { get; protected set; }

        public decimal LitersAvailable { get; protected set; }

        public bool CanBeSaved => throw new NotImplementedException();

        protected Beer()
        { }

        public Beer(decimal literPrice, string description, decimal litersAvailable) 
            : this()
        {
            if (string.IsNullOrEmpty(description)) throw new ArgumentNullException("description");
            if (literPrice <= 0) throw new Exception("literPrice");
            if (litersAvailable <= 0) throw new Exception("litersAvailable");

            LiterPrice = literPrice;
            Description = description;
            LitersAvailable = litersAvailable;
        }

        public void ChargeTank(decimal litersToAdd)
        {
            if (litersToAdd < 0) throw new LitersChargeNegativeException();

            LitersAvailable += litersToAdd;
        }

        public void UnchargeTank(decimal litersToRemove)
        {
            if (litersToRemove == 0) throw new LitersToRemoveIsZeroException();
            if ((this.LitersAvailable - litersToRemove) < 0) throw new LitersToRemoveExcedeedException();

            LitersAvailable -= litersToRemove;
        }

        public void UpdateDescription(string descriptionToChange)
        {
            if (string.IsNullOrEmpty(descriptionToChange)) throw new NullOrEmptyBeerDescriptionException();

            Description = descriptionToChange;
        }

        public void IncreaseLiterPrice(int priceToChange)
        {
            if (LiterPrice + priceToChange < 0) throw new LitersChargeNegativeException();

            LiterPrice += priceToChange;
        }

        public void DecreaseLiterPrice(int priceToChange)
        {
            if (LiterPrice - priceToChange < 0) throw new LitersChargeNegativeException();

            LiterPrice -= priceToChange;
        }

        public BeerSnapshot TakeSnapshop()
        {
            return new BeerSnapshot(Description, LiterPrice, Id);
        }
    }
}
