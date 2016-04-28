using DataAccessLayer.Repository;
using Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServicesWebApi.Controllers
{
    [RoutePrefix("api/reservations")]
    public class ReservationsController : ApiController
    {

        #region Repository

        /// <summary>
        /// Reservations Repository (Access to the DataContext)
        /// </summary>
        private ReservationRepository reservationRepo = new ReservationRepository();

        #endregion

        #region WEB API
        // GET: api/Reservations
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            List<Reservation> reservations;

            reservations = this.reservationRepo.GetAll().ToList();

            if (reservations == null)
                return NotFound();

            return Ok(reservations);
        }


        // GET: api/Reservations/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetReservationByID(int id)
        {
            Reservation res;

            res = this.reservationRepo.GetByID(id);

            if (res == null)
                return NotFound();

            return Ok(res);
        }

        // GET: api/Reservations/Lenzi
        [HttpGet]
        [Route("{clientName:alpha}")]
        public IHttpActionResult GetReservationsByClientName(string clientName)
        {
            List<Reservation> reservations;

            reservations = this.reservationRepo.FindReservationsByClientName(clientName);

            if (reservations == null || reservations.Count == 0)
                return NotFound();

            return Ok(reservations);
        }

        // POST: api/Reservations
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateReservation(Reservation res)
        {
            try
            {
                this.reservationRepo.Create(res);
                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        // PUT: api/Reservations
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateReservation(Reservation res)
        {
            try
            {
                this.reservationRepo.Update(res);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Reservations/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteReservation(int id)
        {
            try
            {
                Reservation res = this.reservationRepo.GetByID(id);

                if (res == null)
                    return NotFound();

                this.reservationRepo.Delete(res);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion
    }
}
