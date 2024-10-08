using GrudgeBookMvc.src.Domain.Book;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GrudgeBookMvc.src.Model.Postgres.Book
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

        public Domain.Book.Grudge ToDomain()
        {
            GrudgeStatus grudgeStatus = GrudgeStatusBuilder.FromString(Status);

            Uri? uri = null;
            if (VisualizationURI != null)
            {
                uri = new Uri(VisualizationURI);
            }

            Domain.Book.Grudge parsedGrudge = new(
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

    public class DBGrudgeAdapter
    {
        public Grudge ToModel(Domain.Book.Grudge unparsedGrudge)
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