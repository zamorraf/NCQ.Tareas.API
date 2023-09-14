using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NCQ.Tareas.API.Datos.Interfaces;
using NCQ.Tareas.API.Modelos;
using NCQ.Tareas.API.Modelos.ViewModels;

namespace NCQ.Tareas.API.Datos
{
    public class ApiRepositorio : IApiRepositorio
    {
        private readonly DatosContext _datosContext;
        private IConfiguration _configuration;

        public ApiRepositorio(DatosContext datosContext, IConfiguration configuration)
        {
            _datosContext = datosContext;
            _configuration = configuration;
        }


        // Colaboradores
        public async Task<IEnumerable<Colaborador>> ObtenerColaboradores()
        {
            var colaboradores = await _datosContext.Colaborador.ToListAsync();
            //var colaboradores = await _datosContext.Colaborador.OrderBy(c => c.NombreCompleto).ToListAsync(); 
            //var colaboradores = await _datosContext.Colaborador.Where( c=> c.NombreCompleto.Contains("Zamora")).ToListAsync();
            //colaboradores = colaboradores.OrderBy(c => c.NombreCompleto).ToList();
            return colaboradores;

        }

        public /*async Task<IEnumerable<Colaborador>>*/ string ObtColaboradores()
        {
            return _configuration["ConnectionStrings:Conexion"];
            /*var colaboradores = await _datosContext.Colaborador.ToListAsync();
            return colaboradores;*/

        }

        public async Task<Colaborador> ObtenerColaborador(int id)
        {
            var colaborador = await _datosContext.Colaborador.FirstOrDefaultAsync(u => u.Id == id);

            return colaborador;
        }


        // Tareas
        public async Task<IEnumerable<Tarea>> ObtenerTareas()
        {
            var tareas = await _datosContext.Tarea.ToListAsync();
            return tareas;
        }

        public IEnumerable<TareaVM> ObtTareas()
        {
            var tareas = new List<TareaVM>();
            int campoColaboradorId;

            //Conección 
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = _configuration["ConnectionStrings:Conexion"];
            conn.Open();

            string strSQL = "SELECT tar.Id ,tar.Descripcion ,tar.ColaboradorId ,tar.Estado ,tar.Prioridad ,tar.FechaInicio ,tar.FechaFin ,tar.Notas " +
                                   ",col.NombreCompleto ColaboradorNombre " +
                                   ",CASE tar.Prioridad WHEN 1 THEN 'Alta' WHEN 2 THEN 'Media' WHEN 3 THEN 'Baja' END PrioridadDesc " +
                                   ",CASE tar.Estado WHEN 1 THEN 'Pendiente' WHEN 2 THEN 'En proceso' WHEN 3 THEN 'Finalizada' END EstadoDesc " +
                            "FROM tarea tar " +
                            "LEFT JOIN colaborador col " +
                            "ON col.Id = tar.ColaboradorId " +
                            "ORDER BY tar.FechaInicio ";

            SqlCommand cmd = new SqlCommand(strSQL, conn);
            cmd.CommandType = System.Data.CommandType.Text;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //campoId = "";
                if ( reader["ColaboradorId"].ToString().IsNullOrEmpty())
                    campoColaboradorId = 0;
                else campoColaboradorId = Convert.ToInt32(reader["ColaboradorId"]);

                tareas.Add(new TareaVM
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Descripcion = reader["Descripcion"].ToString(),
                    //ColaboradorId = Convert.ToInt32(reader["ColaboradorId"]),
                    ColaboradorId = campoColaboradorId,
                    Estado = (byte)Convert.ToInt32(reader["Estado"]),
                    Prioridad = (byte)Convert.ToInt32(reader["Prioridad"]),
                    FechaInicio = Convert.ToDateTime(reader["FechaInicio"]),
                    FechaFin = Convert.ToDateTime(reader["FechaFin"]),
                    Notas = reader["Notas"].ToString(),
                    ColaboradorNombre = reader["ColaboradorNombre"].ToString(),
                    EstadoDesc = reader["EstadoDesc"].ToString(),
                    PrioridadDesc = reader["PrioridadDesc"].ToString()
                });
            }
            conn.Close();

            return tareas;
        }

        

        public async Task<Tarea> ObtenerTarea(int id)
        {
            var tarea = await _datosContext.Tarea.FirstOrDefaultAsync(u => u.Id == id);

            return tarea;
        }

        public TareaVM ObtTarea(int id)
        {
            var tarea = new TareaVM();
            int campoColaboradorId;

            //Conección 
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = _configuration["ConnectionStrings:Conexion"];
            conn.Open();

            string strSQL = "SELECT tar.Id ,tar.Descripcion ,tar.ColaboradorId ,tar.Estado ,tar.Prioridad ,tar.FechaInicio ,tar.FechaFin ,tar.Notas " +
                                   ",col.NombreCompleto ColaboradorNombre " +
                                   ",CASE tar.Prioridad WHEN 1 THEN 'Alta' WHEN 2 THEN 'Media' WHEN 3 THEN 'Baja' END PrioridadDesc " +
                                   ",CASE tar.Estado WHEN 1 THEN 'Pendiente' WHEN 2 THEN 'En proceso' WHEN 3 THEN 'Finalizada' END EstadoDesc " +
                            "FROM tarea tar " +
                            "LEFT JOIN colaborador col " +
                            "ON col.Id = tar.ColaboradorId " +
                            "WHERE tar.id = " + id.ToString();

            SqlCommand cmd = new SqlCommand(strSQL, conn);
            cmd.CommandType = System.Data.CommandType.Text;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (reader["ColaboradorId"].ToString().IsNullOrEmpty())
                        campoColaboradorId = 0;
                    else campoColaboradorId = Convert.ToInt32(reader["ColaboradorId"]);

                    tarea.Id = Convert.ToInt32(reader["Id"]);
                    tarea.Descripcion = reader["Descripcion"].ToString();
                    tarea.ColaboradorId = campoColaboradorId;//Convert.ToInt32(reader["ColaboradorId"]);
                    tarea.Estado = (byte)Convert.ToInt32(reader["Estado"]);
                    tarea.Prioridad = (byte)Convert.ToInt32(reader["Prioridad"]);
                    tarea.FechaInicio = Convert.ToDateTime(reader["FechaInicio"]);
                    tarea.FechaFin = Convert.ToDateTime(reader["FechaFin"]);
                    tarea.Notas = reader["Notas"].ToString();
                    tarea.ColaboradorNombre = reader["ColaboradorNombre"].ToString();
                    tarea.EstadoDesc = reader["EstadoDesc"].ToString();
                    tarea.PrioridadDesc = reader["PrioridadDesc"].ToString();
                }
            }

            conn.Close();

            return tarea;
        }


        // Generales
        public void Agregar<T>(T entity) where T : class
        {
            _datosContext.Add(entity);
        }

        public void Eliminar<T>(T entity) where T : class
        {
            _datosContext.Remove(entity);
        }

        public async Task<bool> Guardar()
        {
            return await _datosContext.SaveChangesAsync() > 0;
        }

    }    
}
