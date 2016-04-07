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
            return View(car);
        }

        // GET: Cars/Create
        public ActionResult CreatePartial()
        {
            return PartialView("_CreatePartial");
        }


        [HttpPost]
        public ActionResult CreatePartial(Car car)
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
            return PartialView("_CreatePartial", car);


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
            return PartialView("_EditPartial", car);
        }
        [HttpPost]
        public ActionResult EditPartial(Car car)
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
            return PartialView("_EditPartial", car);


        }
        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CarID,Doamin,Brand,Model,DailyTariff,AutomaticDrive")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(car);
        }


        // GET: Cars/DeletePartial/5
        public ActionResult DeletePartial(long? id)
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
            return PartialView("_DeletePartial", car);
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
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("DeletePartial")]
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
            return PartialView("_DeletePartial");
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
