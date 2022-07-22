namespace Catalog.DTOs
{
    public record CreateItemDto
    {
        public string Name { get; init; }

        public decimal Price { get; init; }
    }
}