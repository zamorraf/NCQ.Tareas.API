using NCQ.Tareas.API.Modelos;
using NCQ.Tareas.API.Modelos.ViewModels;

namespace NCQ.Tareas.API.Datos.Interfaces
{
    public interface IApiRepositorio
    {
        // Colaboradores
        Task<IEnumerable<Colaborador>> ObtenerColaboradores();
        string ObtColaboradores();

        Task<Colaborador> ObtenerColaborador(int id);

        //Tareas
        Task<IEnumerable<Tarea>> ObtenerTareas();
        IEnumerable<TareaVM> ObtTareas();

        Task<Tarea> ObtenerTarea(int id);
        TareaVM ObtTarea(int id);

        // Generales
        void Agregar<T> (T entity) where T : class;

        void Eliminar<T>(T entity) where T : class;

        Task<bool> Guardar();
    }
}
