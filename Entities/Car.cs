using System;

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
        public Int64 CarID { get; set; }

        /// <summary>
        /// Patente de Auto
        /// </summary>
        public String Doamin { get; set; }

        /// <summary>
        /// Marca del Auto
        /// </summary>
        public String Brand { get; set; }

        /// <summary>
        /// Modelo de Auto
        /// </summary>
        public String Model { get; set; }

        /// <summary>
        /// Tarifa Diaria Vehiculo
        /// </summary>
        public Decimal DailyTariff { get; set; }

        /// <summary>
        /// Indica si posee Caja Automatica
        /// </summary>
        public Boolean AutomaticDrive { get; set; }
    }
}