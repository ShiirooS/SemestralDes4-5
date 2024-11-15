﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Actividades_Semestral.Models;

public partial class PropuestasActividade
{
    
    public int IdPropuesta { get; set; }

    public int? IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int? IdSubcategoria { get; set; }

    public string? Lugar { get; set; }

    public DateOnly? Fecha { get; set; }

    public TimeOnly? Hora { get; set; }

    public int? LimiteCupos { get; set; }

    public decimal? Costo { get; set; }

    public string? Requisitos { get; set; }

    public string? ImagenUrl { get; set; }

    public int? IdEstado { get; set; }

    public string? ComentarioRechazo { get; set; }
    [JsonIgnore]
    public virtual EstadoActividade? IdEstadoNavigation { get; set; }
    [JsonIgnore]
    public virtual Subcategoria? IdSubcategoriaNavigation { get; set; }
    [JsonIgnore]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
