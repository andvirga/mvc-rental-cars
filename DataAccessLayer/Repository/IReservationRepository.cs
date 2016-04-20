using Entities;
using System.Collections.Generic;

namespace DataAccessLayer.Repository
{
    public interface IReservationRepository
    {
        /// <summary>
        /// Finds a list of reservations by Client Name
        /// </summary>
        /// <param name="pClientName">Client First Name or Last Name</param>
        /// <returns>Reservations</returns>
        List<Reservation> FindReservationsByClientName(string pClientName);
    }
}