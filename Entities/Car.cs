using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    /// <summary>
    /// Entidad que representa un vehiculo a alquilar.
    /// </summary>
    public class Car
    {
        /// <summary>
        /// ID de Auto
        /// </summary>
        [Key]
        public Int64 CarID { get; set; }

        /// <summary>
        /// Patente de Auto
        /// </summary>
        [Required]
        [Display(Name = "Patente")]
        [RegularExpression(@"^[a-z]{3}[0-9]{3}$", ErrorMessage = "Patente invalida")]
        public String Doamin { get; set; }

        /// <summary>
        /// Marca del Auto
        /// </summary>
        [Required]
        [Display(Name = "Marca")]
        public String Brand { get; set; }

        /// <summary>
        /// Modelo de Auto
        /// </summary>
        [Required]
        [Display(Name = "Modelo")]
        public String Model { get; set; }

        /// <summary>
        /// Tarifa Diaria Vehiculo
        /// </summary>
        [Required]
        [Display(Name = "Tarifa diaria")]
        public Decimal DailyTariff { get; set; }

        /// <summary>
        /// Indica si posee Caja Automatica
        /// </summary>
        [Display(Name = "Automatico")]
        public Boolean AutomaticDrive { get; set; }
    }
}