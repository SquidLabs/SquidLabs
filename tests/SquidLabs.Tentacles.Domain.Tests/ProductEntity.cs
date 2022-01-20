using SquidLabs.Tentacles.Domain.Objects;

namespace SquidLabs.Tentacles.Domain.Tests;

public class ProductEntity : BaseEntity
{
    public string Name { get; set; }
    public string ModelNumber { get; set; }
    public decimal Price { get; init; }
    public string Description { get; init; }
    public int Quantity { get; set; }

    public void changeAvailableQuantity(int quantity)
    {
        Quantity = quantity;
    }
}