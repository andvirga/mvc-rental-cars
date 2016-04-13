using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Entities;
using DataAccessLayer;
using DataAccessLayer.Repository;

namespace mvc_rental_cars.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        /// <summary>
        /// Reservation Repository
        /// </summary>
        private ReservationRepository reservationRepo = new ReservationRepository();

        /// <summary>
        /// Car Repository
        /// </summary>
        private CarRepository carRepository = new CarRepository();

        /// <summary>
        /// Client Repository
        /// </summary>
        private ClientRepository clientRepository = new ClientRepository();

        // GET: Reservations
        public ActionResult Index()
        {
            //var reservationContext = db.ReservationContext.Include(r => r.Car).Include(r => r.Client);
            //--Calling the Repository to get the list of all the reservations.
            return View(this.reservationRepo.GetAll());
        }

        // GET: Reservations/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //--Calling the Repository to get the Reservation by ID.
            Reservation reservation = this.reservationRepo.GetByID(id.Value);

            if (reservation == null)
                return HttpNotFound();
            
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.CarID = new SelectList(this.carRepository.GetAll(), "CarID", "Domain");
            ViewBag.ClientID = new SelectList(this.clientRepository.GetAll(), "ClientID", "LastName");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservationID,ClientID,CarID,StartDate,EndDate")] Reservation reservation)
        {
            Reservation resCreated = reservation;

            if (ModelState.IsValid)
            {
                //--Calling the Repository to store the Reservation into the DBContext
                resCreated = this.reservationRepo.Create(reservation);
                return RedirectToAction("Index");
            }

            ViewBag.CarID = new SelectList(this.carRepository.GetAll(), "CarID", "Domain", reservation.CarID);
            ViewBag.ClientID = new SelectList(this.clientRepository.GetAll(), "ClientID", "LastName", reservation.ClientID);
            return View(resCreated);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Reservation reservation = this.reservationRepo.GetByID(id.Value);

            if (reservation == null)
                return HttpNotFound();
            
            ViewBag.CarID = new SelectList(this.carRepository.GetAll(), "CarID", "Domain", reservation.CarID);
            ViewBag.ClientID = new SelectList(this.clientRepository.GetAll(), "ClientID", "LastName", reservation.ClientID);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationID,ClientID,CarID,StartDate,EndDate")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                this.reservationRepo.Update(reservation);
                return RedirectToAction("Index");
            }
            ViewBag.CarID = new SelectList(this.carRepository.GetAll(), "CarID", "Domain", reservation.CarID);
            ViewBag.ClientID = new SelectList(this.clientRepository.GetAll(), "ClientID", "LastName", reservation.ClientID);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Reservation reservation = this.reservationRepo.GetByID(id.Value);

            if (reservation == null)
                return HttpNotFound();
            
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Reservation reservation = this.reservationRepo.GetByID(id);

            this.reservationRepo.Delete(reservation);

            return RedirectToAction("Index");
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
