using System;
using System.Collections.Generic;

namespace Entities
{
    /// <summary>
    /// Alquiler (Reserva) de un Auto para un Cliente Determinado
    /// </summary>
    public class Reservation
    {
        /// <summary>
        /// ID de Alquiler
        /// </summary>
        public Int64 ReservationID { get; set; }

        /// <summary>
        /// Auto alquilado
        /// </summary>
        public virtual Car RentedCar { get; set; }

        /// <summary>
        /// Cliente asociado
        /// </summary>
        public virtual Client Client { get; set; }

        /// <summary>
        /// Fecha Inicio del alquiler
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Fecha fin del alquiler
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}