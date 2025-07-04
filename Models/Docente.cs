using System;
using System.Collections.Generic;

namespace Parcial3_sumaran.Models;

public partial class Docente
{
    public int Id { get; set; }

    public string Apellidos { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string? Profesion { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public string? Correo { get; set; }

    public virtual ICollection<Curso> Cursos { get; set; } = new List<Curso>();
}
