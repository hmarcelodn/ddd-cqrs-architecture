using System.Collections.Generic;

namespace eBeerShop.Domain.Beers
{
    public interface IBeerRepository
    {
        List<Beer> GetCatalog();

        Beer RetrieveBeer(int v);
    }
}
