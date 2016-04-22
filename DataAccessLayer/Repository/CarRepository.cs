using Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Data.Entity;

namespace DataAccessLayer.Repository
{
    public class CarRepository : GenericRepository<Car>, ICarRepository
    {
        /// <summary>
        /// Method used to fill the Client Drop Down List.
        /// </summary>
        public List<Car> FillCarDropDownList()
        {
            var carList = this.GetAll().ToList();
            carList.ForEach(c => c.Domain = String.Format("Modelo: {0} {1} - Tarifa: ${2}", c.Brand, c.Model, c.DailyTariff));
            return carList;
        }

        /// <summary>
        /// Find all the reservations for one particular Car
        /// </summary>
        /// <param name="car">Car</param>
        /// <returns>Reservations</returns>
        public List<Reservation> FindCarReservations(Car car)
        {
            ReservationRepository reservationRepo = new ReservationRepository();

            var reservations = reservationRepo.GetAll().ToList();

            var carReservations = from res in reservations
                                  where res.Car.Equals(car)
                                  select res;

            return carReservations.ToList();
        }
    }
}
