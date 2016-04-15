using System;
using System.Data.Entity;
using Entities;

namespace DataAccessLayer
{
    /// <summary>
    /// (Singleton) Application Entity-Framework DBContext (Mapping to DB). Accesible ONLY by 'Instance' property.
    /// </summary>
    /// <remarks>Use RentalCarsDBContext.Instance to access all the methods of this class</remarks>
    public class RentalCarsDBContext : DbContext
    {
        #region DbSets (Context)

        /// <summary>
        /// DbSet del Objeto 'Car'
        /// </summary>
        private DbSet<Car> CarContext { get; set; }

        /// <summary>
        /// DbSet del Objeto 'Client'
        /// </summary>
        private DbSet<Client> ClientContext { get; set; }

        /// <summary>
        /// DbSet del Objeto 'Reservation'
        /// </summary>
        private DbSet<Reservation> ReservationContext { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor (only used in the Instance property)
        /// </summary>
        public RentalCarsDBContext() { }

        #endregion
    }
}
