namespace GrudgeBookMvc.src.Model.Services.BookServices
{
    public class GrudgeDeleteException : Exception
    {
        public GrudgeDeleteException()
        {

        }
        public GrudgeDeleteException(string message)
            : base(message)
        {

        }
        public GrudgeDeleteException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }

    public class IdIsNotFoundException : Exception
    {
        public IdIsNotFoundException()
        {

        }
        public IdIsNotFoundException(string message)
            : base(message)
        {

        }
        public IdIsNotFoundException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}