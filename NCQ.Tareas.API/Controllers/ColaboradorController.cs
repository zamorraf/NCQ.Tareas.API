using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NCQ.Tareas.API.Datos.Interfaces;
using NCQ.Tareas.API.Modelos;

namespace NCQ.Tareas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColaboradorController : ControllerBase
    {

        private readonly IApiRepositorio _repositorio;

        public ColaboradorController(IApiRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var colaboradores = await _repositorio.ObtenerColaboradores();

            return Ok(colaboradores);
        }

        [HttpGet("SQL")]
        public IActionResult ObtColaboradores()
        {
            var str = _repositorio.ObtColaboradores();
            //var colaboradores = await _repositorio.ObtenerColaboradores();

            return Ok(str);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            var colaborador = await _repositorio.ObtenerColaborador(id);
            if (colaborador == null)
            {
                return NotFound("Colaborador no encontrado");
            }

            return Ok(colaborador);
        }


        [HttpPost]
        public async Task<IActionResult> Agregar(Colaborador colaborador)
        {
            _repositorio.Agregar(colaborador);
            if (await _repositorio.Guardar())
            {
                return Ok();
            }
                
            return BadRequest();    
        }


        [HttpPut]
        public async Task<IActionResult> Modificar(Colaborador colaborador)
        {
            var colaboradorActualiza = await _repositorio.ObtenerColaborador(colaborador.Id);

            if (colaboradorActualiza == null)
                return BadRequest();

            colaboradorActualiza.NombreCompleto = colaborador.NombreCompleto;
            if (!await _repositorio.Guardar())
                return NoContent();

            return Ok(colaboradorActualiza);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var colaborador = await _repositorio.ObtenerColaborador(id);
            if (colaborador == null)
                return NotFound("Colaborador no encontrado");

            _repositorio.Eliminar(colaborador);
            if (!await _repositorio.Guardar())
                return BadRequest("No se pudo eliminar el colaborador");

            return Ok("Colaborador borrado");
        }

    }
}
