using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Diagnostics;
using System.Data.Entity;

namespace DataAccessLayer.Repository
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        #region Constructor

        public ReservationRepository() { }

        #endregion

        #region Override CRUD Methods

        /// <summary>
        /// Finds a list of reservations by Client Name
        /// </summary>
        /// <param name="pClientName">Client First Name or Last Name</param>
        /// <returns>Reservations</returns>
        public List<Reservation> FindReservationsByClientName(string pClientName)
        {
            var reservations = this.GetAll().ToList();

            var searchString = pClientName.ToLower();

            var clientRes = from res in reservations
                            where (res.Client.FirstName.ToLower().Contains(searchString) || res.Client.LastName.ToLower().Contains(searchString))
                            select res;

            return clientRes.ToList();
        }

        #endregion
    }
}
