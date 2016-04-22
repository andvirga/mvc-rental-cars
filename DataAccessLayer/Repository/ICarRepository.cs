using Entities;
using System.Collections.Generic;

namespace DataAccessLayer.Repository
{
    public interface ICarRepository
    {
        List<Car> FillCarDropDownList();

        List<Reservation> FindCarReservations(Car car);
    }
}