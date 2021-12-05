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
    public class ShopBridgesController : ApiController
    {

        ShopBridgeDbContext shopBridgeDbContext = new ShopBridgeDbContext();
        // GET: api/ShopBridges
        public IEnumerable<ShopBridge> Get()
        {
            return shopBridgeDbContext.ShopBridge;
        }

        // GET: api/ShopBridges/5
        public IHttpActionResult Get(int id)
        {
            var quote = shopBridgeDbContext.ShopBridge.Find(id);
            if (quote == null)
                return BadRequest("Id doesn't exist");
            return Ok(quote);
        }
        // POST: api/ShopBridges
        public IHttpActionResult Post(ShopBridge value)
        {
            if (!ModelState.IsValid)//If required fields are empty it will hit
                return BadRequest(ModelState);
            
            value.LastModified = DateTime.Now;
            value.CreatedOn = DateTime.Now;
            
            shopBridgeDbContext.ShopBridge.Add(value);
            shopBridgeDbContext.SaveChanges();
            return StatusCode(HttpStatusCode.Created);
        }

        // PUT: api/ShopBridges/5
        public IHttpActionResult Put(int id, ShopBridge data)
        {
            if (!ModelState.IsValid)//If required fields are empty it will hit
                return BadRequest(ModelState);

            var entity = shopBridgeDbContext.ShopBridge.FirstOrDefault(q => q.Id == id);
            if (entity == null)
                return BadRequest("Id doesn't exist");
            entity.Name = data.Name;
            entity.Price = data.Price;
            entity.Description = data.Description;
            shopBridgeDbContext.SaveChanges();
            return Ok("Record added Successfully");
        }

        // DELETE: api/ShopBridges/5
        public IHttpActionResult Delete(int id)
        {
            var quote = shopBridgeDbContext.ShopBridge.Find(id);
            if (quote == null)
                return BadRequest("Id not found");
            shopBridgeDbContext.ShopBridge.Remove(quote);
            shopBridgeDbContext.SaveChanges();
            return Ok("Id has been deleted");
        }
        
        [HttpGet]
        [Route("api/ShopBridges/LoadSorted")]
        public IHttpActionResult LoadSorted(string sort)
        {
            IQueryable<ShopBridge> ShopBridge;
            switch (sort)
            {
                case "asc":
                    ShopBridge = shopBridgeDbContext.ShopBridge.OrderBy(q => q.Name);
                    break;
                case "desc":
                    ShopBridge = shopBridgeDbContext.ShopBridge.OrderByDescending(q => q.Name);
                    break;
                default:
                    ShopBridge = shopBridgeDbContext.ShopBridge;
                    break;
            }
            return Ok(ShopBridge);

        }
        [HttpGet]
        [Route("api/ShopBridges/Paging/{pageNumber=}/{pageSize=}")]
        public IHttpActionResult LoadPaging(int pageNumber, int pageSize)
        {
            var ShopBridge = shopBridgeDbContext.ShopBridge.OrderBy(q => q.Id);
            return Ok(ShopBridge.Skip((pageNumber - 1) * pageSize).Take(pageSize));
        }

        [HttpGet]
        [Route("api/ShopBridges/Search")]
        public IHttpActionResult Search(string name)
        {
            var ShopBridge = shopBridgeDbContext.ShopBridge.Where(q => q.Name.StartsWith(name));
            return Ok(ShopBridge);
        }
    }
}
