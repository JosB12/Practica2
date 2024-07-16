using ApiAplication.Interface;
using ApiAplication.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiAplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactoController : ControllerBase
    {
        private readonly IContacto service;

        public ContactoController(IContacto service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("{userId}/AgregarContacto")]
        public IActionResult AgregarContacto(int userId, [FromBody] Contacto nuevoContacto)
        {
            string resultado = service.AgregarContacto(userId, nuevoContacto);
            JsonSerializer.Serialize(resultado);
            return Ok(resultado);
        }

        [HttpPut]
        [Route("{userId}/EditarContacto")]
        public IActionResult EditarContacto(int userId, [FromBody] Contacto contactoEditado)
        {
            string resultado = service.EditarContacto(userId, contactoEditado);
            return Ok(resultado);
        }

        [HttpDelete]
        [Route("{userId}/EliminarContacto/{contactoId}")]
        public IActionResult EliminarContacto(int userId, int contactoId)
        {
            string resultado = service.EliminarContacto(userId, contactoId);
            return Ok(resultado);
        }

        [HttpGet]
        [Route("{userId}/Contacto/{contactoId}")]
        public IActionResult ObtenerContacto(int userId, int contactoId)
        {
            Contacto contacto = service.ObtenerContacto(userId, contactoId);
            if (contacto == null)
            {
                return NotFound("Contacto no encontrado");
            }
            return Ok(contacto);
        }


        //cuidado abajo
        
        [HttpGet]
        [Route("{userId}/Contactos")]
        public IActionResult ObtenerContactos(int userId)
        {
            List<Contacto> contactos = service.ObtenerContactoslista(userId);
            if (contactos == null)
            {
                return NotFound("Usuario no encontrado");
            }
            return Ok(contactos);
        }
        
    }
}
