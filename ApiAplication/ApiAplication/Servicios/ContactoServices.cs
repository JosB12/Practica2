using ApiAplication.Interface;
using ApiAplication.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiAplication.Servicios
{
    public class ContactoServices : IContacto
    {
        private static string ArchivoJson = "usuarios.json";
        private static List<User> DatosCreados = ConvertirJson();

        public string AgregarContacto(int userId, Contacto nuevoContacto)
        {
            User usuario = DatosCreados.Find(u => u._Id == userId);
            if (usuario == null)
            {
                return "Usuario no encontrado";
            }

            nuevoContacto.ContactoId = usuario.Contactos.Count + 1;
            usuario.Contactos.Add(nuevoContacto);
            GuardarDatosEnJSON(DatosCreados);
            var mensaje = new { messaje = $"Contacto {nuevoContacto.Nombre} agregado al usuario {usuario.Nombre}" };
            return JsonSerializer.Serialize(mensaje);
        }

        public string EditarContacto(int userId, Contacto contactoEditado)
        {
            User usuario = DatosCreados.Find(u => u._Id == userId);
            if (usuario == null)
            {
                return "Usuario no encontrado";
            }

            Contacto contacto = usuario.Contactos.Find(c => c.ContactoId == contactoEditado.ContactoId);
            if (contacto == null)
            {
                return "Contacto no encontrado";
            }

            contacto.Nombre = contactoEditado.Nombre;
            contacto.Telefono = contactoEditado.Telefono;
            contacto.Email = contactoEditado.Email;
            contacto.Direccion = contactoEditado.Direccion;
            GuardarDatosEnJSON(DatosCreados);
            var mensaje = new { message = $"Contacto {contacto.Nombre} editado" };
            return JsonSerializer.Serialize(mensaje);
        }

        public string EliminarContacto(int userId, int contactoId)
        {
            User usuario = DatosCreados.Find(u => u._Id == userId);
            if (usuario == null)
            {
                return "Usuario no encontrado";
            }

            Contacto contacto = usuario.Contactos.Find(c => c.ContactoId == contactoId);
            if (contacto == null)
            {
                return "Contacto no encontrado";
            }

            usuario.Contactos.Remove(contacto);
            GuardarDatosEnJSON(DatosCreados);
            return $"Contacto {contacto.Nombre} eliminado";
        }

        public Contacto ObtenerContacto(int userId, int contactoId)
        {
            User usuario = DatosCreados.Find(u => u._Id == userId);
            if (usuario == null)
            {
                return null;
            }

            return usuario.Contactos.FirstOrDefault(c => c.ContactoId == contactoId);
        }

        //cuidado abajo
        
        public List<Contacto> ObtenerContactoslista(int userId)
        {
            User usuario = DatosCreados.Find(u => u._Id == userId);
            if (usuario == null)
            {
                return null;
            }
            return usuario.Contactos;
        }
        
        private static List<User> ConvertirJson()
        {
            if (!System.IO.File.Exists(ArchivoJson))
            {
                return new List<User>();
            }

            using (StreamReader sw = new StreamReader(ArchivoJson))
            {
                string Archjson = sw.ReadToEnd();
                return JsonSerializer.Deserialize<List<User>>(Archjson);
            }
        }

        private static void GuardarDatosEnJSON(List<User> users)
        {
            string Archjson = JsonSerializer.Serialize(users);

            using (StreamWriter sr = new StreamWriter(ArchivoJson))
            {
                sr.Write(Archjson);
            }
        }
    }
}
