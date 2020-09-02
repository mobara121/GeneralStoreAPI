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
    public class CustomerController : ApiController
    {
        //field
        private readonly StoreDbContext _context = new StoreDbContext();

        //--Create(POST)
        [HttpPost]
        public async Task<IHttpActionResult> PostCustomer(Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Your request body cannot be empty.");
            }
            if (ModelState.IsValid)
            {
                _context.Customer.Add(customer);
                await _context.SaveChangesAsync();

                return Ok("You added a customer and it was securely saved!");
            }

            return BadRequest(ModelState);
        }

        //--Read(GET)
        // Get by ID
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            Customer customer = await _context.Customer.FindAsync(id);

            if (customer != null)
            {
                return Ok(customer);
            }
            return NotFound(); //404 Error to be returned                                                                                                           
        }
        // Get All
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Customer> customers = await _context.Customer.ToListAsync();
            return Ok(customers);
        }

        //Update(PUT)
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCustomers([FromUri] int id, [FromBody] Customer updatedCustomer)
        {
            if (ModelState.IsValid)
            {
                Customer customer = await _context.Customer.FindAsync(id);

                if (customer != null)
                {
                    customer.FirstName = updatedCustomer.FirstName;
                    customer.LastName = updatedCustomer.LastName;
                    //customer.FullName = updatedCustomer.FullName;

                    await _context.SaveChangesAsync(); //updated data to be saved

                    return Ok("Customer has been updated.");
                }

                //the customer wasn't found
                return NotFound(); //404 Error to be returned 

            }

            //Return a bad request (request info wasn't enough?)
            return BadRequest(ModelState);
        }

        //Delete(DELETE)
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCustomerById(int id)
        {
            Customer entity = await _context.Customer.FindAsync(id);

            if (entity == null)
            {
                return NotFound(); //404 Error
            }

            _context.Customer.Remove(entity);

            if (await _context.SaveChangesAsync() == 1) //How many change was made? --only 1 time.
            {
                return Ok("The customer info was deleted.");
            }

            return InternalServerError();// 500 error
        }
    }
}
