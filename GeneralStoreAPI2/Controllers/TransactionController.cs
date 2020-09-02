using GeneralStoreAPI2.Models;
using System;
using System.Collections.Generic;
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
        public async Task<IHttpActionResult> PostTransaction(Transaction model)
        {
            if (Item)
            {

            }
        }
        //Get All (GET)

        //Get All by CustomerID (GET)

        //Get a transaction by its ID (GET)

        //Update by its ID (PUT)

        //Delete by its ID (DELETE)
    }
}
