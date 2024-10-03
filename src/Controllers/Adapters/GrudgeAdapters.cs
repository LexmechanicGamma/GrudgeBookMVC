using GrudgeBookMvc.src.Model.Domain.Book;
using GrudgeBookMvc.src.Views.Json.Book;

namespace GrudgeBookMvc.src.Controllers.Adapters
{
    public static class GrudgeAdapters
    {
        public static Model.Domain.Book.Grudge ToDomain(Views.Json.Book.Grudge grudge)
        {
            GrudgeStatus grudgeStatus = GrudgeStatusBuilder.FromString(grudge.Status);

            try
            {
                DateTime time = DateTimeOffset.
                    FromUnixTimeSeconds(
                        Convert.ToInt64(grudge.Timestamp)
                    ).UtcDateTime;

                if (grudge.VisualizationURI == null)
                {
                    Model.Domain.Book.Grudge parsedGrudge = new(
                    grudge.Id,
                    grudge.TitleOfSin,
                    time,
                    grudge.Condition,
                    grudge.Details,
                    grudgeStatus,
                    null);

                    return parsedGrudge;
                }
                else
                {
                    Model.Domain.Book.Grudge parsedGrudge = new(
                    grudge.Id,
                    grudge.TitleOfSin,
                    time,
                    grudge.Condition,
                    grudge.Details,
                    grudgeStatus,
                    new Uri(grudge.VisualizationURI));

                    return parsedGrudge;
                }
            }
            catch (FormatException e)
            {
                throw new InvalidUnixTimestampException(e.Message + "Date must be in UnixTimeCode");
            }
        }
        public static Views.Json.Book.Grudge FromDomain(Model.Domain.Book.Grudge grudge)
        {
            string parsedTimestamp = grudge.WickedHour.ToString();

            string visualEvidence = null!;
            if (grudge.VisualEvidence != null)
            {
                visualEvidence = grudge.VisualEvidence.ToString();
            }

            Views.Json.Book.Grudge parsedGrudge = new Views.Json.Book.Grudge
            {
                Id = grudge.Id,
                TitleOfSin = grudge.TitleOfSin,
                Timestamp = parsedTimestamp,
                Condition = grudge.ConditionOfVengence,
                Details = grudge.DetailsOfTransgression,
                Status = Enum.GetName(grudge.Status)!,
                VisualizationURI = visualEvidence
            };

            return parsedGrudge;
        }

        public static List<Views.Json.Book.Grudge> ListParsedGrudges(List<Model.Domain.Book.Grudge> grudges)
        {
            List<Views.Json.Book.Grudge> parsedGrudges = new();
            foreach (var grudge in grudges)
            {
                parsedGrudges.Add(FromDomain(grudge));
            }
            return parsedGrudges;
        }
    }
}