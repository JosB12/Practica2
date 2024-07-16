using ApiAplication.Model;

namespace ApiAplication.Interface
{
    public interface IContacto
    {
        string AgregarContacto(int userId, Contacto nuevoContacto);
        string EditarContacto(int userId, Contacto contactoEditado);
        string EliminarContacto(int userId, int contactoId);
         List<Contacto> ObtenerContactoslista(int userId); //cuidado
        public Contacto ObtenerContacto(int userId, int contactoId);

    }

}
