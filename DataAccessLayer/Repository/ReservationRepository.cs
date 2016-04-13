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

        #endregion
    }
}
