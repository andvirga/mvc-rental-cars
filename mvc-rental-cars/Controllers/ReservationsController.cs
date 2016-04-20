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
        public const String ValidationErrorMessage = "Error de Validación. Por favor revise los datos ingresados";

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

            return PartialView("Details", reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.CarID = new SelectList(this.carRepository.FillCarDropDownList(), "CarID", "Domain");
            ViewBag.ClientID = new SelectList(this.clientRepository.FillClientDropDownList(), "ClientID", "LastName");
            return PartialView("Create");
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
                return Json(new { success = true });
            }
            else
            {
                ModelState.AddModelError("", ValidationErrorMessage);
            }

            ViewBag.CarID = new SelectList(this.carRepository.FillCarDropDownList(), "CarID", "Domain", reservation.CarID);
            ViewBag.ClientID = new SelectList(this.clientRepository.FillClientDropDownList(), "ClientID", "LastName", reservation.ClientID);

            return PartialView("Create", resCreated);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Reservation reservation = this.reservationRepo.GetByID(id.Value);

            if (reservation == null)
                return HttpNotFound();

            ViewBag.CarID = new SelectList(this.carRepository.FillCarDropDownList(), "CarID", "Domain", reservation.CarID);
            ViewBag.ClientID = new SelectList(this.clientRepository.FillClientDropDownList(), "ClientID", "LastName", reservation.ClientID);
            return PartialView("Edit", reservation);
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
                return Json(new { success = true });
            }
            ViewBag.CarID = new SelectList(this.carRepository.FillCarDropDownList(), "CarID", "Domain", reservation.CarID);
            ViewBag.ClientID = new SelectList(this.clientRepository.FillClientDropDownList(), "ClientID", "LastName", reservation.ClientID);
            return PartialView("Edit", reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Reservation reservation = this.reservationRepo.GetByID(id.Value);

            if (reservation == null)
                return HttpNotFound();
            
            return PartialView("Delete", reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Reservation reservation = this.reservationRepo.GetByID(id);

            this.reservationRepo.Delete(reservation);

            return Json(new { success = true });
        }

        /// <summary>
        /// Search for all the reservations for a particular Client
        /// </summary>
        /// <param name="clientName">Client First Name or Last Name</param>
        /// <returns>Reservations</returns>
        public ActionResult FindReservation(string clientName)
        {
            List<Reservation> reservations = this.reservationRepo.FindReservationsByClientName(clientName);

            return PartialView("FindReservations", reservations);
        }

        /// <summary>
        /// Dìspose the Controller and the Repositories
        /// </summary>
        /// <param name="disposing">Dispose Enable/Disable</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.clientRepository.Dispose();
                this.carRepository.Dispose();
                this.reservationRepo.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
