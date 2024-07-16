namespace ApiAplication.Model
{
    public class User
    {
        public int _Id { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public List<Contacto> Contactos { get; set; } = new List<Contacto>();

    }
}
