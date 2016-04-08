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

namespace mvc_rental_cars.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        //--Contexto
        private RentalCarsDBContext db = new RentalCarsDBContext();

        // GET: Clients
        public ActionResult Index()
        {
            return View(db.ClientContext.ToList());   
        }

        // GET: Clients/Details/5
        public ActionResult Details(long? id)
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
            if (ModelState.IsValid)
            {
                try
                {
                    db.ClientContext.Add(client);
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                    return Json(new { success = true });
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }

            }
            //Something bad happened
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
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(client).State = EntityState.Modified;
                    db.SaveChanges();

                    return Json(new { success = true });
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }

            }

            //Something bad happened
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
            try
            {
                Client client = db.ClientContext.Find(id);
                db.ClientContext.Remove(client);
                db.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
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
