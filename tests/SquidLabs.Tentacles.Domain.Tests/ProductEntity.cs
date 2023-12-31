using SquidLabs.Tentacles.Domain.Objects;

namespace SquidLabs.Tentacles.Domain.Tests;

public class ProductEntity : Entity
{
    public string Name { get; set; } = null!;
    public string ModelNumber { get; set; } = null!;
    public decimal Price { get; init; }
    public string Description { get; init; } = default!;
    public int Quantity { get; set; }

    public void ChangeAvailableQuantity(int quantity)
    {
        Quantity = quantity;
    }
}