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
using DataAccessLayer.Repository;

namespace mvc_rental_cars.Controllers
{
    [Authorize]
    public class CarsController : Controller
    {
        public const String ValidationErrorMessage = "Error de Validación. Por favor revise los datos ingresados";

        /// <summary>
        /// Car Repository
        /// </summary>
        private CarRepository carRepository = new CarRepository();

        // GET: Cars
        public ActionResult Index()
        {
            return View(this.carRepository.GetAll());
        }

        // GET: Cars/Details/5
        public ActionResult Details(long? id)
        {
            Car car = new Car();

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            car = this.carRepository.GetByID(id.Value);

            if (car == null)
                return HttpNotFound();

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
            Car carCreated = car;

            //--If there isn't any validation errors, save the model into the context.
            if (ModelState.IsValid)
            {
                carCreated = this.carRepository.Create(car);
                return Json(new { success = true });
            }
            else
            {
                //--If a validation error occurs add it to the ModelState.
                ModelState.AddModelError("", ValidationErrorMessage);
            }

            return PartialView("Create", carCreated);
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Car car = this.carRepository.GetByID(id.Value);

            if (car == null)
                return HttpNotFound();

            return PartialView("Edit", car);
        }

        [HttpPost]
        public ActionResult Edit(Car car)
        {
            //--If there isn't any validation errors, save the model into the context.
            if (ModelState.IsValid)
            {
                this.carRepository.Update(car);
                return Json(new { success = true });
            }
            //--If a validation error occurs add it to the ModelState.
            ModelState.AddModelError("", ValidationErrorMessage);

            return PartialView("Edit", car);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Car car = this.carRepository.GetByID(id.Value);

            if (car == null)
                return HttpNotFound();

            return PartialView("Delete", car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            //--Deleting the model into the context.
            Car car = this.carRepository.GetByID(id);
            this.carRepository.Delete(car);
            return Json(new { success = true });
        }

        /// <summary>
        /// Dìspose the Controller and the Repositories
        /// </summary>
        /// <param name="disposing">Dispose Enable/Disable</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                this.carRepository.Dispose();
            
            base.Dispose(disposing);
        }
    }
}
