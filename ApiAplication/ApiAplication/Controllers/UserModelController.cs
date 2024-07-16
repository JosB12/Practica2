using ApiAplication.Interface;
using ApiAplication.Model;
using ApiAplication.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Text.Json;

namespace ApiAplication.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserModelController : ControllerBase
    {
        private readonly IPersona service;

        public UserModelController(IPersona service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("GuardarUsuario")]
        public IActionResult PostDatosRegistros([FromBody] User NuevoUser)
        {
            String results = service.PostDatosRegistros(NuevoUser);
            if (results == "Datos erroneos")
            {
                return BadRequest(results);
            }
            return CreatedAtAction(nameof(GetDatosCreados),
            new { id = NuevoUser._Id }, NuevoUser);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult PostLogear([FromBody] User user)
        {
            User Registro = (User)service.PostLogear(user);
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
            User registro =  (User)service.GetDatosCreados(id);
            if (registro == null)
            {
                return NotFound();
            }

            return Ok(registro);
        }
    }
}