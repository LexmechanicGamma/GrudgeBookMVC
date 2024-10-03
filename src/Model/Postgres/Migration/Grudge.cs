using System;
using System.Collections.Generic;

namespace GrudgeBookMvc.src.Model.Postgres.Migration;

public partial class Grudge
{
    public string Id { get; set; } = null!;

    public string TitleOfSin { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public string Condition { get; set; } = null!;

    public string Details { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? VisualizationUri { get; set; }
}
