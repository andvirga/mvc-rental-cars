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
        public HttpResponseMessage PostClient(Client client)
        {
            client = db.ClientContext.Add(client);
            db.SaveChanges();
            var response = new HttpResponseMessage(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, "/api/clients" + client.ClientID.ToString());
            return response;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public Client DeleteClient(int id)
        {
            Client client = db.ClientContext.Find(id);
            if (client == null)
            {
              throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            db.ClientContext.Remove(client);
            db.SaveChanges();
            return client;
        }
    }
}
