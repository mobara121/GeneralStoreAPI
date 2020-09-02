using GeneralStoreAPI2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GeneralStoreAPI2.Controllers
{
    public class ProductController : ApiController
    {
        private readonly StoreDbContext _context = new StoreDbContext();

        // Create new products
        [HttpPost]
        public async Task<IHttpActionResult> CreateProduct(Product model)
        {
            // Check to see if the model is found or the request content is right
            if (model == null)
            {
                return BadRequest("Your request cannot be empty.");
            }

            // Check to see if the model is NOT valid
            if (!ModelState.IsValid)
            {
                _context.Product.Add(model);
                await _context.SaveChangesAsync();

                return Ok("You added a product and it was securely saved!");
            }

            return BadRequest(ModelState);
        }

        // Read(GET)
            // Get All
        //[Route("api/Product/GetAll")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Product> products = await _context.Product.ToListAsync();
            return Ok(products);
        }

        // Get by SKU
        [Route("api/Product/{SKU}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetBySku(string SKU)
        {
            Product product = await _context.Product.FindAsync(SKU);

            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();

        }
        // Update(PUT)
        //[Route("api/Product/UpdateProduct/{SKU}")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateProduct([FromUri]string SKU, [FromBody]Product updateProduct)
        {
            if (ModelState.IsValid)
            {
                Product product = await _context.Product.FindAsync(SKU);

                if (product != null)
                {
                    product.Name = updateProduct.Name;
                    product.Cost = updateProduct.Cost;
                    product.NumberInInventory = updateProduct.NumberInInventory;
                    //product.IsInStock = updateProduct.IsInStock;

                    await _context.SaveChangesAsync();

                    return Ok("Product has been updated.");
                }

                return NotFound();
            }

            return BadRequest(ModelState);
        }

        // Delete(DELETE)
        
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteProductBySku(string SKU)
        {
            Product entity = await _context.Product.FindAsync(SKU);

            if (entity == null)
            {
                return NotFound();
            }

            _context.Product.Remove(entity);

            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok("The product was deleted.");
            }

            return InternalServerError();
        }

        //[Route("api/Product/GetRandomInt")]
        //public int GetRandomIdInt()
        //{
        //    Random rand = new Random();
        //    return rand.Next();
        //}
    }
}
