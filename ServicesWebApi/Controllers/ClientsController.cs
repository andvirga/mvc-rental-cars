﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities;
using DataAccessLayer;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Web.Http.Cors;

namespace ServicesWebApi.Controllers
{
    [EnableCors(origins: "https://localhost:44300", headers: "*", methods: "*")]
    public class ClientsController : ApiController
    {
        //--Contexto
        private RentalCarsDBContext db = new RentalCarsDBContext();
        //GET api/values
        
        public string GetClients()
        {
           return JsonConvert.SerializeObject(db.ClientContext.ToList());
        }
      
        #region Paging
        public IEnumerable<Client> GetClients(int pageIndex, int pageSize)
        {
            return db.ClientContext.ToList().Skip(pageIndex * pageSize).Take(pageSize);

        }

        #endregion 
       
        // GET api/values/5
        public string Get(int id)
        {
           
            Client client = db.ClientContext.Find(id);
            if (client == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return JsonConvert.SerializeObject(client);
        }

        // POST api/values
        public HttpResponseMessage PostClient(Client client)
        {
            client = db.ClientContext.Add(client);
            db.SaveChanges();
            var response = Request.CreateResponse<Client>(HttpStatusCode.Created,client);
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