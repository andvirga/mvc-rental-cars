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
    }
}
