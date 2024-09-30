using GrudgeBookMvc.src.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace GrudgeBookMvc.src.Model.Postgres
{
    public class Grudge 
    {
        public string Id { get; set; }
        public string TitleOfSin { get; set; }
        public DateTime Timestamp { get; set; }
        public string Condition { get; set; }
        public string Details { get; set; }
        public string Status { get; set; }
        public string? VisualizationURI { get; set; } = null;

        public Domain.Grudge ToDomain()
        {
            GrudgeStatus grudgeStatus = GrudgeStatusBuilder.FromString(Status);

            Uri? uri = null;
            if (VisualizationURI != null)
            {
                uri = new Uri(VisualizationURI);
            }

            Domain.Grudge parsedGrudge = new(
            Id,
            TitleOfSin,
            Timestamp,
            Condition,
            Details,
            grudgeStatus,
            uri);

            return parsedGrudge;
        }
    }

    public class GrudgeContext : DbContext
    {
        public DbSet<Grudge> grudges { get; set; }
        public GrudgeContext(DbContextOptions<GrudgeContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }

    public class DBGrudgeAdapter
    {
        public Grudge ToModel(Domain.Grudge unparsedGrudge)
        {
            string parsedStatus = Enum.GetName(unparsedGrudge.Status)!;

            string uri;
            if (unparsedGrudge.VisualEvidence != null)
                uri = unparsedGrudge.VisualEvidence.ToString();

            Grudge parsedGrudge = new()
            {
                Id = unparsedGrudge.Id,
                TitleOfSin = unparsedGrudge.TitleOfSin,
                Timestamp = unparsedGrudge.WickedHour,
                Condition = unparsedGrudge.ConditionOfVengence,
                Details = unparsedGrudge.DetailsOfTransgression,
                Status = parsedStatus,
                VisualizationURI = null
            };

            return parsedGrudge;
        }
    }
}