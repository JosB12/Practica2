using ApiAplication.Model;
using Microsoft.AspNetCore.Mvc;

namespace ApiAplication.Interface
{
    public interface IPersona
    {
        public String PostDatosRegistros([FromBody] User NuevoUser);

        public User PostLogear([FromBody] User user);

        public User GetDatosCreados(int id);


    }
}
