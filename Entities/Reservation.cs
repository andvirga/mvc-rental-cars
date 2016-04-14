using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    /// <summary>
    /// Alquiler (Reserva) de un Auto para un Cliente Determinado
    /// </summary>
    public class Reservation
    {
        [Key]
        /// <summary>
        /// ID de Alquiler
        /// </summary>
        public Int64 ReservationID { get; set; }

        /// <summary>
        /// Id del Cliente
        /// </summary>
        [Required]
        public Int64 ClientID { get; set; }

        /// <summary>
        /// ID de Auto
        /// </summary>
        [Required]
        public Int64 CarID { get; set; }

        /// <summary>
        /// Auto alquilado
        /// </summary>
        public virtual Car Car { get; set; }

        /// <summary>
        /// Cliente asociado
        /// </summary>
        public virtual Client Client { get; set; }

        /// <summary>
        /// Fecha Inicio del alquiler
        /// </summary>
        [Required]
        [Display(Name = "Fecha Inicio de Reserva")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Fecha fin del alquiler
        /// </summary>
        [Required]
        [Display(Name = "Fecha Fin de Reserva")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EndDate { get; set; }
    }
}