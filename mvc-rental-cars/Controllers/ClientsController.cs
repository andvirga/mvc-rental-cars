using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using Entities;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DataAccessLayer.Repository;

namespace mvc_rental_cars.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        public const String ValidationErrorMessage = "Error de Validación. Por favor revise los datos ingresados";

        /// <summary>
        /// Client Repository
        /// </summary>
        private ClientRepository clientRepository = new ClientRepository();

        // GET: Clients
        public async Task<ActionResult> Index()
        {
            List<Client> clients = new List<Client>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44301/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync("api/clients");
                if (response.IsSuccessStatusCode)
                {
                    string clientJSON = await response.Content.ReadAsAsync<string>();
                    clients = JsonConvert.DeserializeObject<List<Client>>(clientJSON);

                }
            }
            return View(clients.ToList());
        }

        // GET: Clients/Details/5
        public ActionResult Details(long? id)
        {
            Client client = new Client();

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //--Searching the client into the context.
            client = this.clientRepository.GetByID(id.Value);

            if (client == null)
                return HttpNotFound();

            return PartialView("Details", client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return PartialView("Create");
        }

        [HttpPost]
        public async Task<ActionResult> Create(Client client)
        {

            //--If there isn't any validation errors, save the model into the context.
            if (ModelState.IsValid)
            {
                using (var createClient = new HttpClient())
                {
                    createClient.BaseAddress = new Uri("http://localhost:31014/");
                    createClient.DefaultRequestHeaders.Accept.Clear();
                    createClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await createClient.PostAsJsonAsync("api/clients", client);
                    // HTTP GET

                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { success = true });
                    }
                }

            }
            //--If a validation error occurs add it to the ModelState.
            ModelState.AddModelError("", ValidationErrorMessage);

            return PartialView("Create", client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Client client = this.clientRepository.GetByID(id.Value);

            if (client == null)
                return HttpNotFound();

            return PartialView("Edit", client);
        }

        [HttpPost]
        public ActionResult Edit(Client client)
        {

            //--If there isn't any validation errors, save the model into the context.
            if (ModelState.IsValid)
            {
                this.clientRepository.Update(client);
                return Json(new { success = true });
            }
            //--If a validation error occurs add it to the ModelState.
            ModelState.AddModelError("", ValidationErrorMessage);

            return PartialView("Edit", client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Client client = this.clientRepository.GetByID(id.Value);

            if (client == null)
                return HttpNotFound();

            return PartialView("Delete", client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            //--Deleting the model into the context.

            Client client = this.clientRepository.GetByID(id);
            this.clientRepository.Delete(client);
            return Json(new { success = true });
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
