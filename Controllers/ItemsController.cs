using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using Catalog.DTOs;
using Catalog.Entities;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController: ControllerBase
    {
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }

        // Get /items
        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            var items = repository.GetItems().Select(item => item.AsDto());

            return items;
        }

        // Get /items/{id}
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repository.GetItem(id);
    
            if (item is null)
            {
                return NotFound();
            }

            return item.AsDto();
        }

        // Post /items
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow,
            };

            repository.CreateItem(item);

            return CreatedAtAction(nameof(GetItem), new{ id = item.Id }, item.AsDto());
        }
    }
}