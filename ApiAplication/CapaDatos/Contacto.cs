﻿using System;
using System.Collections.Generic;

namespace CapaDatos;

public partial class Contacto
{
    public int ContactoId { get; set; }

    public string? Nombre { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public string? Direccion { get; set; }

    public int UsuarioId { get; set; } //prueba

}