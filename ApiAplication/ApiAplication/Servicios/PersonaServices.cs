using ApiAplication.Interface;
using ApiAplication.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiAplication.Servicios
{
    public class PersonaServices : IPersona
    {
        private static string ArchivoJson = "usuarios.json";
        private static List<User> DatosCreados = ConvertirJson();
        //private static int IncId = 1;
        private static int IncId = ObtenerUltimoId() + 1; //probando este cuidado el de arriba es el original


        public String PostDatosRegistros([FromBody] User NuevoUser)
        {
            if (NuevoUser == null || string.IsNullOrEmpty(NuevoUser.Nombre)
                || string.IsNullOrEmpty(NuevoUser.Password))
            {
                return "Datos erroneos";
            }

            NuevoUser._Id = IncId++;
            DatosCreados.Add(NuevoUser);
            GuardarDatosEnJSON(DatosCreados);
            GuardarUltimoId(IncId); // Guardar el último ID utilizado (esta es una prueba)

            return $"Usuario {NuevoUser.Nombre} creado con el id {NuevoUser._Id} ";

            
        }


        public User PostLogear([FromBody] User user)
        {
            User registro = DatosCreados.Find(usu => usu.Nombre
            == user.Nombre && usu.Password == user.Password)!;
            
            return registro;
        }


        public User GetDatosCreados(int id)
        {
            User registro = DatosCreados.Find(usu => usu._Id == id)!;

            return registro;
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
                return JsonSerializer.Deserialize<List<User>>(Archjson)!;
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

        //probando lo de abajo cuidado
        private static int ObtenerUltimoId()
        {
            if (!File.Exists("id.txt"))
            {
                return 0;
            }

            string idStr = File.ReadAllText("id.txt");
            if (int.TryParse(idStr, out int ultimoId))
            {
                return ultimoId;
            }

            return 0;
        }

        private static void GuardarUltimoId(int ultimoId)
        {
            File.WriteAllText("id.txt", ultimoId.ToString());
        }
    }
}

