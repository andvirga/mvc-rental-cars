using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mvc_rental_cars.Startup))]
namespace mvc_rental_cars
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
