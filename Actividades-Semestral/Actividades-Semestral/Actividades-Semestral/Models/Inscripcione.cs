﻿using System;
using System.Collections.Generic;

namespace Actividades_Semestral.Models;

public partial class Inscripcione
{
    public int IdInscripcion { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdActividad { get; set; }

    public DateTime? FechaInscripcion { get; set; }

    public int? IdEstado { get; set; }

    public virtual Actividade? IdActividadNavigation { get; set; }

    public virtual EstadoActividade? IdEstadoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
