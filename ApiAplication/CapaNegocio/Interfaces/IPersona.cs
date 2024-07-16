using CapaDatos;
namespace ApiAplication.Interface
{
    public interface IPersona
    {
        public String PostDatosRegistros( Usuario NuevoUser);

        public Usuario PostLogear( Usuario user);

        public Usuario GetDatosCreados(int id);


    }
}
