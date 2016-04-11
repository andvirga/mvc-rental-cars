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

namespace mvc_rental_cars.Controllers
{
    public class CarsController : Controller
    {
        private RentalCarsDBContext db = new RentalCarsDBContext();

        public const String ValidationErrorMessage = "Error de Validación. Por favor revise los datos ingresados";
        public const String ExceptionErrorMessage = "Ocurrió una excepción no esperada. Contactar con Departamento Sistemas";

        // GET: Cars
        public ActionResult Index()
        {
            return View(db.CarContext.ToList());
        }

        // GET: Cars/Details/5
        public ActionResult Details(long? id)
        {
            Car car = new Car();
            try
            {
                if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                //--Searching the client into the context.
                car = db.CarContext.Find(id);

                if (car == null)
                    return HttpNotFound();
            }
            catch (Exception e)
            {
                //--If an exception occurs, show the message and add it to EventViewer.
                ModelState.AddModelError("", ExceptionErrorMessage);
                EventLog.WriteEntry("Details", e.Message, EventLogEntryType.Error);
            }
            return PartialView("Details", car);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            return PartialView("Create");
        }


        [HttpPost]
        public ActionResult Create(Car car)
        {
            try
            {
                //--If there isn't any validation errors, save the model into the context.
                if (ModelState.IsValid)
                {
                    db.CarContext.Add(car);
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
            try
            {
                //--If there isn't any validation errors, save the model into the context.
                if (ModelState.IsValid)
                {
                    db.Entry(car).State = EntityState.Modified;
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
            //--Deleting the model into the context.
            try
            {
                Car car = db.CarContext.Find(id);
                db.CarContext.Remove(car);
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
