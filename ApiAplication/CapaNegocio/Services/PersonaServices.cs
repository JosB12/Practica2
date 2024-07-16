using ApiAplication.Interface;
using CapaDatos;
using System.Text.Json;

namespace ApiAplication.Servicios
{
    public class PersonaServices : IPersona
    {
        private CrudContext db;

        public PersonaServices(CrudContext db)
        {
            this.db = db;
        }

        public String PostDatosRegistros(Usuario NuevoUsuario)
        {
            if (NuevoUsuario == null || string.IsNullOrEmpty(NuevoUsuario.Nombre)
        || string.IsNullOrEmpty(NuevoUsuario.Pass))
            {
                return "Datos erroneos";
            }

            db.Usuarios.Add(NuevoUsuario);
            db.SaveChanges();

            return $"Usuario {NuevoUsuario.Nombre} creado con el id {NuevoUsuario.Id}";
            
        }


        public Usuario PostLogear(Usuario Usuario)
        {
            

            
                Usuario registro = db.Usuarios
                    .FirstOrDefault(usu => usu.Nombre == Usuario.Nombre && usu.Pass == Usuario.Pass);

                if (registro == null)
                {
                    return null;
                }
                return registro;
            
        }


        public Usuario GetDatosCreados(int id)
        {
           
                Usuario registro = db.Usuarios
                    .FirstOrDefault(Usuario => Usuario.Id == id);
                return registro;
            
        }


    }
}

