using System;
using System.Collections.Generic;

namespace CapaDatos;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Pass { get; set; }

    public virtual List<Contacto> Contactos { get; set; } = new List<Contacto>();
}
