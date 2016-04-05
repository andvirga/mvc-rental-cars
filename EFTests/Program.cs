using System;
using System.Linq;
using Entities;
using DataAccessLayer;

namespace EFTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //--Instanciamos el Contexto.
            using (var db = new RentalCarsDBContext())
            {
                // Create and save a new Client
                Console.WriteLine("enter the client ID");
                var clientID = Convert.ToInt64(Console.ReadLine());

                Console.Write("Enter the Client FirstName: ");
                var firstName = Console.ReadLine();

                Console.Write("Enter the Client LastName: ");
                var lastName = Console.ReadLine();

                Console.Write("Enter the Client Email: ");
                var email = Console.ReadLine();

                var Client = new Client(clientID, firstName, lastName, email);

                //--Añadimos al ClientContext el Objeto 'Cliente' creado anteriormente.
                db.ClientContext.Add(Client);
                db.SaveChanges();

                // Display all Blogs from the database 
                //var query = from c in db.ClientContext
                //            orderby c.LastName
                //            select c;

                var listaClientes = db.ClientContext.ToList();

                Console.WriteLine("All Clients in the database:");
                foreach (var item in listaClientes)
                {
                    Console.WriteLine("Cliente: {0} {1}", item.FirstName, item.LastName);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
