using ApiAplication.Interface;
using CapaDatos;
using Microsoft.AspNetCore.Mvc;

namespace ApiAplication.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioModelController : ControllerBase
    {

        IPersona persona;

        public UsuarioModelController(IPersona service)
        {
            this.persona = service;
        }

        [HttpPost]
        [Route("GuardarUsuario")]
        public IActionResult PostDatosRegistros([FromBody] Usuario NuevoUsuario)
        {
            String results = persona.PostDatosRegistros(NuevoUsuario);
            if (results == "Datos erroneos")
            {
                return BadRequest(results);
            }
            return CreatedAtAction(nameof(GetDatosCreados), new { id = NuevoUsuario.Id }, NuevoUsuario);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult PostLogear([FromBody] Usuario Usuario)
        {
            Usuario Registro = (Usuario)persona.PostLogear(Usuario);
            if (Registro == null)
            {
                return Unauthorized();
            }
            return Ok(Registro);
              
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetDatosCreados(int id)
        {
            Usuario registro =  (Usuario)persona.GetDatosCreados(id);
            if (registro == null)
            {
                return NotFound();
            }

            return Ok(registro);
        }
    }
}