using InvenTrackPro.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InvenTrackPro.API.Models;
using Microsoft.EntityFrameworkCore;

namespace InvenTrackPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VariationController : ControllerBase
    {
        private readonly InventoryContext _context;

        public VariationController(InventoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Models.Output.ProductVariationDTO>>> GetProductVariations() { 
            var variations = await (from v in  _context.ProductVariations
                                    select new Models.Output.ProductVariationDTO {
                                        VariationId = v.ProductVariationId,
                                        VariationName = v.ProductVariationName,
                                        VariationDescription = v.ProductVariationDescription,
                                        AvailableStock = v.AvailableStock,
                                        VariationPrice = v.Product.Price + v.PriceDelta
                                    }).ToListAsync();

            return Ok(variations);
        }

        [HttpPost]
        public async Task<ActionResult<Models.Input.ProductVariationDTO>> AddProductVariation(Models.Input.ProductVariationDTO variation)
        {
            var newVariation = new Models.ProductVariation
            {
                ProductVariationName = variation.Name,
                ProductVariationDescription = variation.Description,
                AvailableStock = variation.AvailableStock,
                PriceDelta = variation.PriceDelta,
                ProductId = variation.ProductId,
            };

            await _context.ProductVariations.AddAsync(newVariation);

            Models.Product product = _context.Products.Where(x => x.ProductId == newVariation.ProductId).FirstOrDefault();

            product.Variations.Add(newVariation);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(AddProductVariation), newVariation);
        }
    }
}
