using GrudgeBookMvc.src.Model.Domain;
using GrudgeBookMvc.src.Views.Json;

namespace GrudgeBookMvc.src.Controllers.Adapters
{
    public static class GrudgeAdapters
    {
        public static Model.Domain.Grudge ToDomain(Views.Json.Grudge grudge)
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
                    Model.Domain.Grudge parsedGrudge = new(
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
                    Model.Domain.Grudge parsedGrudge = new(
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
        public static Views.Json.Grudge FromDomain(Model.Domain.Grudge grudge)
        {
            string parsedTimestamp = grudge.WickedHour.ToString();

            string visualEvidence = null!;
            if (grudge.VisualEvidence != null)
            {
                visualEvidence = grudge.VisualEvidence.ToString();
            }

            Views.Json.Grudge parsedGrudge = new Views.Json.Grudge
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

        public static List<Views.Json.Grudge> ListParsedGrudges(List<Model.Domain.Grudge> grudges)
        {
            List<Views.Json.Grudge> parsedGrudges = new();
            foreach (var grudge in grudges)
            {
                parsedGrudges.Add(FromDomain(grudge));
            }
            return parsedGrudges;
        }
    }
}