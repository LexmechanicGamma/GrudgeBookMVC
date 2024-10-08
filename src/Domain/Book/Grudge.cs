namespace GrudgeBookMvc.src.Domain.Book
{
    public class Grudge
    {
        public string Id { get; init; }
        public string TitleOfSin { get; set; } = "";
        public DateTime WickedHour { get; set; }
        public string ConditionOfVengence { get; set; } = "";
        public string DetailsOfTransgression { get; set; } = "";
        public GrudgeStatus Status { get; set; } = GrudgeStatus.unsettled;
        public Uri VisualEvidence { get; set; }
        public Grudge(
            string titleOfSin,
            DateTime time,
            string conditionToRepay,
            string details,
            Uri visualEvidence
            )
        {
            Id = Guid.NewGuid().ToString();
            TitleOfSin = titleOfSin;
            WickedHour = time;
            DetailsOfTransgression = details;
            ConditionOfVengence = conditionToRepay;
            VisualEvidence = visualEvidence;
        }

        public Grudge(
            string id,
            string titleOfSin,
            DateTime time,
            string conditionToRepay,
            string details,
            GrudgeStatus status,
            Uri visualEvidence
            )
        {
            if (id == null) Id = Guid.NewGuid().ToString();
            else Id = id;
            TitleOfSin = titleOfSin;
            WickedHour = time;
            DetailsOfTransgression = details;
            ConditionOfVengence = conditionToRepay;
            Status = status;
            VisualEvidence = visualEvidence;
        }
    }
}