using ApiAplication.Interface;
using CapaDatos;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiAplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactoController : ControllerBase
    {
        IContacto contacto;



        public ContactoController(IContacto contacto)
        {
            this.contacto = contacto;
        }

        [HttpPost]
        [Route("{userId}/AgregarContacto")]
        public IActionResult AgregarContacto(int userId, [FromBody] Contacto nuevoContacto)
        {
            string resultado = contacto.AgregarContacto(userId, nuevoContacto);
            return Ok(resultado);
        }




        [HttpPut]
        [Route("{userId}/EditarContacto")]
        public IActionResult EditarContacto(int userId, [FromBody] Contacto contactoEditado)
        {
            string resultado = contacto.EditarContacto(userId, contactoEditado);
            if (resultado.Contains("no encontrado"))
            {
                return NotFound(resultado);
            }
            return Ok(resultado);
        }


        [HttpDelete]
        [Route("{userId}/EliminarContacto/{contactoId}")]
        public IActionResult EliminarContacto(int userId, int contactoId)
        {
            string resultado = contacto.EliminarContacto(userId, contactoId);
            if (resultado.Contains("no encontrado"))
            {
                return NotFound(resultado);
            }
            return Ok(resultado);
        }



        [HttpGet]
        [Route("{userId}/Contactos")]
        public IActionResult ObtenerContactos(int userId)
        {
            var contactos = contacto.ObtenerContactoslista(userId);
            if (contactos == null || !contactos.Any())
            {
                return NotFound(new { message = "No se encontraron contactos para este usuario." });
            }
            return Ok(contactos);
        }

        [HttpGet]
        [Route("{userId}/Contacto/{contactoId}")]
        public IActionResult ObtenerContacto(int userId, int contactoId)
        {
            Contacto contactos = contacto.ObtenerContacto(userId, contactoId);
            if (contactos == null)
            {
                return NotFound("Contacto no encontrado");
            }
            return Ok(contactos);
        }

    }
}
