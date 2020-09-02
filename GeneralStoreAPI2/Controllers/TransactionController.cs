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
    public class TransactionController : ApiController
    {
        //field
        private readonly StoreDbContext _context = new StoreDbContext();

        //Create(POST)
        [HttpPost]
        public async Task<IHttpActionResult> PostTransaction(Transaction transaction)
        {
            if (transaction == null)
            {
                return BadRequest("Your request cannot be empty.");
            }

            if (ModelState.IsValid && transaction.CustomerId !=0 && transaction.ProductSKU != null)
            {
                _context.Transaction.Add(transaction);
                await _context.SaveChangesAsync();

                return Ok("You added a transaction and it was securely saved!");
            }
            return BadRequest(ModelState);
        }
        //Get All (GET)
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Transaction> transactions = await _context.Transaction.ToListAsync();
            if (transactions.Count != 0)
            {
                return Ok(transactions);
            }
            return BadRequest("Your database contains no transactions.");
        }

        //Get All by CustomerID (GET)
        //[Route("api/Transaction/{customerId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetByCustomerId(int CustomerId)
        {
            Product transactions = await _context.Product.FindAsync(CustomerId);

            if (transactions == null)
            {
                return NotFound();
            }
            return Ok(transactions);

        }
        //Get a transaction by its ID (GET)

        //Update by its ID (PUT)

        //Delete by its ID (DELETE)
    }
}
