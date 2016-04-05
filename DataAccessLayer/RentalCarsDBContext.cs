using System;
using System.Data.Entity;
using Entities;

namespace DataAccessLayer
{
    /// <summary>
    /// Clase que contiene el Contexto de Conexion a EF.
    /// </summary>
    public class RentalCarsDBContext : DbContext
    {
        /// <summary>
        /// DbSet del Objeto 'Car'
        /// </summary>
        public DbSet<Car> CarContext { get; set; }

        /// <summary>
        /// DbSet del Objeto 'Client'
        /// </summary>
        public DbSet<Client> ClientContext { get; set; }

        /// <summary>
        /// DbSet del Objeto 'Reservation'
        /// </summary>
        public DbSet<Reservation> ReservationContext { get; set; }
    }
}
