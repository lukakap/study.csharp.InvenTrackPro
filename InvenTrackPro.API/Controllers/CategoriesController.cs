using InvenTrackPro.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InvenTrackPro.API.Models;
using Microsoft.EntityFrameworkCore;
using InvenTrackPro.API.Models.Output;

namespace InvenTrackPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly InventoryContext _context;

        public CategoriesController(InventoryContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Models.Output.CategoryDTO>>> GetCategories() {
            var categories = await (from c in _context.Categories
                                    select new CategoryDTO {
                                        CategoryId = c.CategoryId,
                                        Name = c.Name,
                                        Description = c.Description
                                    }).ToListAsync();

            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult<Models.Input.CategoryDTO>> AddCategory(Models.Input.CategoryDTO category)
        {
            var newCategory = new Models.Category { Name = category.Name, Description = category.Description };
            _context.Categories.Add(newCategory);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(AddCategory), newCategory);
        }
    }
}
