using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Parcial3_sumaran.Models;

public partial class Curso
{
    public int Id { get; set; }

    public string Curso1 { get; set; } = null!;

    public int Creditos { get; set; }

    public int HorasSemanal { get; set; }

    public string? Ciclo { get; set; }

    public int? IdDocente { get; set; }

    [JsonIgnore]
    public virtual Docente? IdDocenteNavigation { get; set; }
}
