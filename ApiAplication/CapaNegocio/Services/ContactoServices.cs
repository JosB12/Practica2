using ApiAplication.Interface;
using CapaDatos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Diagnostics.Metrics;
using System.Text.Json;

namespace ApiAplication.Servicios
{
    public class ContactoServices : IContacto
    {
        private CrudContext db;
        public ContactoServices(CrudContext db)
        {
            this.db = db;
        }

        public String AgregarContacto(int UsuarioId, Contacto nuevoContacto)
        {
            Usuario usuario = db.Usuarios.FirstOrDefault(usu => usu.Id == UsuarioId);
            if (usuario == null)
            {
                return "Usuario no encontrado";
            }

            nuevoContacto.UsuarioId = usuario.Id;
            db.Contactos.Add(nuevoContacto);
            db.SaveChanges();

            var mensaje = new { mensaje = $"Contacto {nuevoContacto.Nombre} agregado al usuario {usuario.Nombre}" };
            return JsonSerializer.Serialize(mensaje);
        }



        public string EditarContacto(int UsuarioId, Contacto contactoEditado)
        {
     
            Usuario usuario = db.Usuarios.FirstOrDefault(usu => usu.Id == UsuarioId);
            if (usuario == null)
            {
                return "Usuario no encontrado";
            }


            Contacto contacto = ObtenerContacto(UsuarioId, contactoEditado.ContactoId);
            if (contacto == null)
            {
                return "Contacto no encontrado";
            }

         
            contacto.Nombre = contactoEditado.Nombre;
            contacto.Telefono = contactoEditado.Telefono;
            contacto.Email = contactoEditado.Email;
            contacto.Direccion = contactoEditado.Direccion;

           
            db.SaveChanges();

            var mensaje = new { mensaje = $"Contacto {contactoEditado.Nombre} editado al usuario {usuario.Nombre}" };
            return JsonSerializer.Serialize(mensaje);
        }


        public string EliminarContacto(int UsuarioId, int contactoId)
        {
           
            Usuario usuario = db.Usuarios .Include(u => u.Contactos)
                .FirstOrDefault(usu => usu.Id == UsuarioId);

            if (usuario == null)
            {
                return "Usuario no encontrado";
            }

            Contacto contacto = usuario.Contactos.FirstOrDefault(cont => cont.ContactoId == contactoId);
            if (contacto == null)
            {
                return "Contacto no encontrado";
            }

            db.Contactos.Remove(contacto);
            db.SaveChanges();

            return "Contacto eliminado correctamente";
        }



        public Contacto ObtenerContacto(int UsuarioId, int contactoId)
        {
            Usuario usuario = db.Usuarios.Include(u => u.Contactos).FirstOrDefault(usu => usu.Id == UsuarioId);
            if (usuario == null)
            {
                return null;
            }

            return usuario.Contactos.FirstOrDefault(c => c.ContactoId == contactoId);
        }


        public List<Contacto> ObtenerContactoslista(int UsuarioId)
        {
            var usuario = db.Usuarios.Include(u => u.Contactos)
                .FirstOrDefault(usu => usu.Id == UsuarioId);

            if (usuario == null)
            {
                return null;
            }

            return usuario.Contactos;
        }


    }
}
