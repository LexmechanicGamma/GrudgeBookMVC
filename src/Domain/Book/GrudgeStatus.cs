namespace GrudgeBookMvc.src.Domain.Book
{
    public enum GrudgeStatus
    {
        repaid,
        forgiven,
        unsettled
    }

    public static class GrudgeStatusBuilder
    {
        public static GrudgeStatus FromString(string str)
        {
            try
            {
                GrudgeStatus status = (GrudgeStatus)Enum.Parse(typeof(GrudgeStatus), str);
                return status;
            }
            catch (ArgumentNullException e)
            {
                throw new StatusParseException(e.Message + "Status field is Empty.");
            }
            catch (ArgumentException e)
            {
                throw new StatusParseException(e.Message +
                    "Incorrect Status-Field Input: It must be - repaid,forgiven or unsettled.");
            }
            catch (OverflowException e)
            {
                throw new StatusParseException(e.Message);
            }
        }
    }

}