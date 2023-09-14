using Microsoft.EntityFrameworkCore;
using NCQ.Tareas.API.Modelos;

namespace NCQ.Tareas.API.Datos
{
    public class DatosContext : DbContext
    {
        public DatosContext(DbContextOptions<DatosContext> options) : base(options) 
        {
            
        }

        public DbSet<Tarea> Tarea { get; set; }
        public DbSet<Colaborador> Colaborador { get; set;}

    }
}
