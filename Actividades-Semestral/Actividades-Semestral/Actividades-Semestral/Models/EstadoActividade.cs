using System;
using System.Collections.Generic;

namespace Actividades_Semestral.Models;

public partial class EstadoActividade
{
    public int IdEstado { get; set; }

    public string NombreEstado { get; set; } = null!;

    public int? IdTipoEstado { get; set; }

    public virtual ICollection<Actividade> Actividades { get; set; } = new List<Actividade>();

    public virtual TipoEstado? IdTipoEstadoNavigation { get; set; }

    public virtual ICollection<Inscripcione> Inscripciones { get; set; } = new List<Inscripcione>();

    public virtual ICollection<PropuestasActividade> PropuestasActividades { get; set; } = new List<PropuestasActividade>();
}
