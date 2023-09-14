using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NCQ.Tareas.API.Datos.Interfaces;
using NCQ.Tareas.API.Modelos;

namespace NCQ.Tareas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly IApiRepositorio _repositorio;

        public TareaController(IApiRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var tareas = await _repositorio.ObtenerTareas();

            return Ok(tareas);
        }

        [HttpGet("SQL")]
        public IActionResult ObtTareas()
        {
            var tareas = _repositorio.ObtTareas();

            return Ok(tareas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            var tarea = await _repositorio.ObtenerTarea(id);
            if (tarea == null)
            {
                return NotFound("Tarea no encontrado");
            }

            return Ok(tarea);
        }

        [HttpGet("SQL/{id}")]
        public IActionResult ObtTarea(int id)
        {
            var tarea = _repositorio.ObtTarea(id);

            return Ok(tarea);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(Tarea tarea)
        {
            _repositorio.Agregar(tarea);
            if (await _repositorio.Guardar())
            {
                return Ok();
            }

            return BadRequest();
        }


        [HttpPut]
        public async Task<IActionResult> Modificar(Tarea tarea)
        {
            var tareaActualiza = await _repositorio.ObtenerTarea(tarea.Id);

            if (tareaActualiza == null)
                return BadRequest();

            tareaActualiza.Descripcion = tarea.Descripcion;
            tareaActualiza.ColaboradorId = tarea.ColaboradorId;
            tareaActualiza.Estado = tarea.Estado;
            tareaActualiza.Prioridad = tarea.Prioridad;
            tareaActualiza.FechaInicio = tarea.FechaInicio;
            tareaActualiza.FechaFin = tarea.FechaFin;
            tareaActualiza.Notas = tarea.Notas;
            if (!await _repositorio.Guardar())
                return NoContent();

            return Ok(tareaActualiza);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var tarea = await _repositorio.ObtenerTarea(id);
            if (tarea == null)
                return NotFound("Taea no encontrada");

            _repositorio.Eliminar(tarea);
            if (!await _repositorio.Guardar())
                return BadRequest("No se pudo eliminar la tarea");

            return Ok("Tarea eliminada");
        }

    }
}
