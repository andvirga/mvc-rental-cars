using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        /// <summary>
        /// Method used to fill the Client Drop Down List.
        /// </summary>
        public List<Client> FillClientDropDownList()
        {
            var clientList = this.GetAll().ToList();
            clientList.ForEach(c => c.LastName = String.Concat(c.LastName, ", ", c.FirstName));
            return clientList;
        }
    }
}
