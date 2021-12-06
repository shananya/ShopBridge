using ShopBridge_API.Data;
using ShopBridge_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopBridge_API.Controllers
{
    public class ErrorsController : ApiController
    {
        ShopBridgeDbContext errorDbContext = new ShopBridgeDbContext();

        /// <summary>
        /// Fetch Errors
        /// </summary>
        /// <param></param>
        /// <returns>Errors</returns>
        public IEnumerable<Error> Get()
        {
            return errorDbContext.Error;
        }

        /// <summary>
        /// Post Erros
        /// </summary>
        /// <param value="value">The error string</param>
        /// <returns>Status Code</returns>
        // POST: api/Errors
        public IHttpActionResult Post(Error value)
        {
            if (!ModelState.IsValid)//If required fields are empty it will hit
                return BadRequest(ModelState);
            
            value.CreatedOn = DateTime.Now;

            errorDbContext.Error.Add(value);
            errorDbContext.SaveChanges();
            return StatusCode(HttpStatusCode.Created);
        }
    }
}
