

using Microsoft.EntityFrameworkCore; //Importacion del DbContext del paquete nuget
using tokesMagistralService.WebApi.Models; //Importacion de los modelos 

namespace tokesMagistralService.WebApi
{

    //DEBE SER HEREDADO DE DB CONTEXT
    //CADA VEZ QUE UN MODELO SE AGREGUE DEBE SER MAPEADO EN EL DBContext
    public class DataDbContext : DbContext //Nos permitrira definir las tablas que se creeran en la BD
    {
        //Creamos constructor
        public DataDbContext(DbContextOptions<DataDbContext> options ): base(options)//Parametros con injeccion de dependecias

        {
            
        }
        //Entity framework reconozca nuestro modelo y lo lleve a la BD
        //Por regla en BD se pasa de singular a plural
        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ServiceVehicle> Services { get; set; }
    }
}
