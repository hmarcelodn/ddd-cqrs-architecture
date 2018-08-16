namespace eBeerShop.Domain.Shared
{
    public interface IAggregateRoot
    {
        bool CanBeSaved { get; }
    }
}
