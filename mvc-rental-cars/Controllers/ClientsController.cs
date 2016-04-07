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
            return PartialView("_DetailsPartial", client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return PartialView("_CreatePartial");
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientID,FirstName,LastName,Email")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.ClientContext.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return PartialView("_CreatePartial", client);
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
            return PartialView("_EditPartial", client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientID,FirstName,LastName,Email")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return PartialView("_EditPartial", client);
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
            return PartialView("_DeletePartial", client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Client client = db.ClientContext.Find(id);
            db.ClientContext.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
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
