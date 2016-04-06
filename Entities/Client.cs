using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    /// <summary>
    /// Entidad que representa un cliente
    /// </summary>
    public class Client
    {
        #region Properties

        /// <summary>
        /// Id del Cliente
        /// </summary>
        public Int64 ClientID { get; set; }

        /// <summary>
        /// Primer Nombre
        /// </summary>
        
        [Display(Name = "Nombre")]
        public String FirstName { get; set; }

        /// <summary>
        /// Apellido
        /// </summary>
        [Display(Name = "Apellido")]
        public String LastName { get; set; }

        /// <summary>
        /// E-Mail
        /// </summary>
        [Display(Name = "Email")]
        public String Email { get; set; }

        /// <summary>
        /// Alquileres del Cliente
        /// </summary>
        [Display(Name = "Alquileres")]
        public virtual List<Reservation> Reservations { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Constructor
        /// </summary>
        public Client() { }

        public Client(long clientID, string firstName, string lastName, string email)
        {
            this.ClientID = clientID;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
        }

        #endregion
    }
}
