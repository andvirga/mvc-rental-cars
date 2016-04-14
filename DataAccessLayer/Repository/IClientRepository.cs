using Entities;
using System.Collections.Generic;

namespace DataAccessLayer.Repository
{
    public interface IClientRepository
    {
        List<Client> FillClientDropDownList();
    }
}