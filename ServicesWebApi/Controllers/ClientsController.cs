using System;
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
using DataAccessLayer.Repository;

namespace ServicesWebApi.Controllers
{
    [EnableCors(origins: "https://localhost:44300", headers: "*", methods: "*")]
    public class ClientsController : ApiController
    {
        /// <summary>
        /// Client Repository (Access to the DataContext)
        /// </summary>
        private ClientRepository clientRepository = new ClientRepository();

        //GET api/values
        public string GetClients()
        {
           return JsonConvert.SerializeObject(this.clientRepository.GetAll());
        }
      
        #region Paging
        public IEnumerable<Client> GetClients(int pageIndex, int pageSize)
        {
            return this.clientRepository.GetAll().Skip(pageIndex * pageSize).Take(pageSize);

        }

        #endregion 
       
        // GET api/values/5
        public string Get(int id)
        {

            Client client = this.clientRepository.GetByID(id);
            if (client == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return JsonConvert.SerializeObject(client);
        }

        // POST api/values
        public HttpResponseMessage PostClient(Client client)
        {
            client = this.clientRepository.Create(client);

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
            Client client = this.clientRepository.GetByID(id);

            if (client == null)
              throw new HttpResponseException(HttpStatusCode.NotFound);

            this.clientRepository.Delete(client);

            return client;
        }

    }
}
