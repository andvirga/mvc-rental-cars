using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities;
using DataAccessLayer;
using Newtonsoft.Json;

namespace ServicesWebApi.Controllers
{
   
    public class ClientsController : ApiController
    {
        //--Contexto
        private RentalCarsDBContext db = new RentalCarsDBContext();
        //GET api/values
        public IEnumerable<Client> GetClients()
        {
            return db.ClientContext.ToList();
        }
        
        // GET api/values/5
        public Client Get(int id)
        {
           
            Client client = db.ClientContext.Find(id);
            if (client == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return client;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
