using InvenTrackPro.API.Data;
using InvenTrackPro.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvenTrackPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly InventoryContext _context;

        public ProductsController(InventoryContext context) {
            _context = context;
        }
    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Output.ProductDTO>>> GetProducts()
        {
            var products = await (from prod in _context.Products
                                  select new Models.Output.ProductDTO
                                  {
                                      ProductId = prod.ProductId,
                                      ProductName = prod.ProductName,
                                      ProductDescription = prod.ProductDescription,
                                      Price = prod.Price,
                                      AvailableStock = prod.AvailableStock,
                                      CategoryName = prod.Category.Name,
                                      Variations = prod.Variations.Select(variation => new Models.Output.ProductVariationDTO
                                      {
                                          VariationId = variation.ProductVariationId,
                                          VariationName = variation.ProductVariationName,
                                          VariationDescription = variation.ProductVariationDescription,
                                          AvailableStock = variation.AvailableStock,
                                          VariationPrice = prod.Price + variation.PriceDelta
                                      }).ToList() // Materialize Variations
                                  }).ToListAsync(); // Execute the main query asynchronously

            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<Models.Input.ProductDTO>> AddProduct(Models.Input.ProductDTO product) 
        {
            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState);
            }

            Models.Product newProduct;

            if (product.CategoryId == null)
            {
                newProduct = new Models.Product
                {
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    Price = product.Price,
                    AvailableStock = product.AvailableStock
                };
            }
            else {
                newProduct = new Models.Product
                {
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    Price = product.Price,
                    AvailableStock = product.AvailableStock,
                    CategoryId = (int)product.CategoryId
                };
            }

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(AddProduct), new { id = newProduct.ProductId}, newProduct);

        }
    }
}
