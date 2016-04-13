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

namespace mvc_rental_cars.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        //--Contexto
        private RentalCarsDBContext db = new RentalCarsDBContext();

        public const String ValidationErrorMessage = "Error de Validación. Por favor revise los datos ingresados";
        public const String ExceptionErrorMessage = "Ocurrió una excepción no esperada. Contactar con Departamento Sistemas";

        // GET: Clients
        public async Task<ActionResult> Index()
        {
            List<Client> clients = new List<Client>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:31014/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync("api/clients");
                if (response.IsSuccessStatusCode)
                {
                    clients = await response.Content.ReadAsAsync<List<Client>>();

                }
            }
            return View(clients.ToList());
        }

        // GET: Clients/Details/5
        public ActionResult Details(long? id)
        {
            Client client = new Client();
            try
            {
                if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                
                //--Searching the client into the context.
                client = db.ClientContext.Find(id);

                if (client == null)
                    return HttpNotFound();
            }
            catch (Exception e)
            {
                //--If an exception occurs, show the message and add it to EventViewer.
                ModelState.AddModelError("", ExceptionErrorMessage);
                EventLog.WriteEntry("Details", e.Message, EventLogEntryType.Error);
            }
            return PartialView("Details", client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return PartialView("Create");
        }

        [HttpPost]
        public ActionResult Create(Client client)
        {
            try
            {
                //--If there isn't any validation errors, save the model into the context.
                if (ModelState.IsValid)
                {
                    db.ClientContext.Add(client);
                    db.SaveChanges();
                    return Json(new { success = true });
                }
                //--If a validation error occurs add it to the ModelState.
                ModelState.AddModelError("", ValidationErrorMessage);
            }
            catch (Exception e)
            {
                //--If an exception occurs, show the message and add it to EventViewer.
                ModelState.AddModelError("", ExceptionErrorMessage);
                EventLog.WriteEntry("Create", e.Message, EventLogEntryType.Error);
            }
            return PartialView("Create", client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.ClientContext.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return PartialView("Edit", client);
        }

        [HttpPost]
        public ActionResult Edit(Client client)
        {
            try
            {
                //--If there isn't any validation errors, save the model into the context.
                if (ModelState.IsValid)
                {
                    db.Entry(client).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true });
                }
                //--If a validation error occurs add it to the ModelState.
                ModelState.AddModelError("", ValidationErrorMessage);
            }
            catch (Exception e)
            {
                //--If an exception occurs, show the message and add it to EventViewer.
                ModelState.AddModelError("", ExceptionErrorMessage);
                EventLog.WriteEntry("Edit", e.Message, EventLogEntryType.Error);
            }
            return PartialView("Edit", client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.ClientContext.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return PartialView("Delete", client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            //--Deleting the model into the context.
            try
            {
                Client client = db.ClientContext.Find(id);
                db.ClientContext.Remove(client);
                db.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                //--If an exception occurs, show the message and add it to EventViewer.
                ModelState.AddModelError("", ExceptionErrorMessage);
                EventLog.WriteEntry("Delete", e.Message, EventLogEntryType.Error);
            }
            return PartialView("Delete");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
