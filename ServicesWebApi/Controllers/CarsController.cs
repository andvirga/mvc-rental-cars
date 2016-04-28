using DataAccessLayer.Repository;
using Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;

namespace ServicesWebApi.Controllers
{
    [RoutePrefix("api/cars")]
    public class CarsController : ApiController
    {
        #region Repository

        /// <summary>
        /// Cars Repository (Access to the DataContext)
        /// </summary>
        private CarRepository carRepository = new CarRepository();

        #endregion

        #region WEB API Actions

        // GET: api/Cars
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            List<Car> cars;

            cars = this.carRepository.GetAll().ToList();

            if (cars == null)
                return NotFound();

            return Ok(cars);
        }

        // GET: api/Cars/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetCarByID(int id)
        {
            Car car;

            car = this.carRepository.GetByID(id);

            if (car == null)
                return NotFound();

            return Ok(car);
        }

        // POST: api/Cars
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateCar(Car car)
        {
            try
            {
                this.carRepository.Create(car);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Cars/5
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateCar(Car car)
        {
            try
            {
                this.carRepository.Update(car);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Cars/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteCar(int id)
        {
            try
            {
                Car car = this.carRepository.GetByID(id);

                if (car == null)
                    return NotFound();

                this.carRepository.Delete(car);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Cars/{id}/reservations
        [Route("{id:int}/reservations")]
        public IHttpActionResult GetCarReservations(int id)
        {
            try
            {
                List<Reservation> reservations;

                Car car = this.carRepository.GetByID(id);

                if (car == null)
                    return NotFound();

                reservations = this.carRepository.FindCarReservations(car);

                if (reservations == null)
                    return NotFound();

                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion
    }
}
