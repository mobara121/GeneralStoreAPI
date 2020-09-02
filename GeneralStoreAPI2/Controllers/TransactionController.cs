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
        //[HttpGet]
        //public async Task<IHttpActionResult> GetByTransactionId(int Id)
        //{
        //    Product transactions = await _context.Product.FindAsync(Id);

        //    if (transactions == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(transactions);

        //}

        //Update by its ID (PUT)
        [HttpPut]
        public async Task<IHttpActionResult> UpdateTransaction([FromUri]int CustomerId, [FromBody]Transaction updateTransaction)
        {
            if (ModelState.IsValid)
            {
                Transaction transaction = await _context.Transaction.FindAsync(CustomerId);
                Customer customer = await _context.Customer.FindAsync(updateTransaction.CustomerId);
                Product product = await _context.Product.FindAsync(updateTransaction.ProductSKU);

                if (transaction != null)
                {
                    if (customer == null)
                    {
                        return BadRequest("Customer not found.");
                    }
                    if (product == null)
                    {
                        return BadRequest("Product not found.");
                    }
                    transaction.ProductSKU = updateTransaction.ProductSKU;
                    transaction.Product = updateTransaction.Product;
                    
                    if (updateTransaction.ItemCount > product.NumberInInventory) //important!!
                    {
                        return BadRequest($"Not Enough {product.Name} items in stock.\n" +
                            $"Please enter a quantity less than {product.NumberInInventory + 1} ");
                    }
                    transaction.ItemCount = updateTransaction.ItemCount;

                    await _context.SaveChangesAsync();

                    return Ok("Transaction has been updated.");
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }
        //Delete by its ID (DELETE)
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteTransactionById(int CustomerId)
        {
            Transaction transaction = await _context.Transaction.FindAsync(CustomerId);

            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transaction.Remove(transaction);

            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok("The transaction was deleted.");
            }

            return InternalServerError();
        }
    }
}
