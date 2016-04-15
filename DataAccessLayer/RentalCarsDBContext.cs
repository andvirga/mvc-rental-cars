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

        #region Singleton Implementation

        /// <summary>
        /// Object to Instantiate
        /// </summary>
        private static RentalCarsDBContext rentalCarsDBContext = null;

        /// <summary>
        /// Constructor (only used in the Instance property)
        /// </summary>
        protected RentalCarsDBContext() { }

        /// <summary>
        /// Property that access to the Singleton Instance of the RentalCarsDBContext Object.
        /// </summary>
        public static RentalCarsDBContext Instance
        {
            get
            {
                //-- The object is instantiated one single time, and then returned every time that we access to this property.
                if (rentalCarsDBContext == null)
                    rentalCarsDBContext = new RentalCarsDBContext();

                return rentalCarsDBContext;
            }
        }

        #endregion
    }
}
