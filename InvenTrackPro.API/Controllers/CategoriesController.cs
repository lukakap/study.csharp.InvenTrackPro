using InvenTrackPro.API.Data;
using Microsoft.AspNetCore.Mvc;
using InvenTrackPro.API.Models;
using Microsoft.EntityFrameworkCore;
using InvenTrackPro.API.Models.Output;
using InvenTrackPro.API.Models.Input;

namespace InvenTrackPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly InventoryContext _context;

        public CategoriesController(InventoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<CategoryOutputDTO>>> GetCategories()
        {
            var categories = await (from c in _context.Categories
                                    select new CategoryOutputDTO
                                    {
                                        CategoryId = c.CategoryId,
                                        Name = c.Name,
                                        Description = c.Description
                                    }).ToListAsync();

            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryInputDTO>> AddCategory(CategoryInputDTO category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newCategory = new Category { Name = category.Name, Description = category.Description };
            _context.Categories.Add(newCategory);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(AddCategory), new { id = newCategory.CategoryId }, newCategory);
        }
    }
}
