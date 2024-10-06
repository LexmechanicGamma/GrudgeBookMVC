namespace GrudgeBookMvc.src.Controllers.BookController
{
    public class InvalidUnixTimestampException : Exception
    {
        public InvalidUnixTimestampException()
        {

        }
        public InvalidUnixTimestampException(string message)
            : base(message)
        {

        }
        public InvalidUnixTimestampException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}