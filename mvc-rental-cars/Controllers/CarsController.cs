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
    public class CarsController : Controller
    {
        private RentalCarsDBContext db = new RentalCarsDBContext();

        // GET: Cars
        public ActionResult Index()
        {
            return View(db.CarContext.ToList());
        }

        // GET: Cars/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.CarContext.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return PartialView("Details",car);
        }
   
        // GET: Cars/Create
        public ActionResult Create()
        {
            return PartialView("Create");
        }


        [HttpPost]
        public ActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.CarContext.Add(car);
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
            return PartialView("Create", car);


        }

        // GET: Cars/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.CarContext.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return PartialView("Edit", car);
        }

        [HttpPost]
        public ActionResult Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(car).State = EntityState.Modified;
                    db.SaveChanges();

                    return Json(new { success = true });
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }

            }

            //Something bad happened
            return PartialView("Edit", car);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(long? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.CarContext.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return PartialView("Delete", car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            try
            {
                Car car = db.CarContext.Find(id);
                db.CarContext.Remove(car);
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
