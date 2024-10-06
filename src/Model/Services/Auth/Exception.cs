namespace GrudgeBookMvc.src.Model.Services.Auth
{
    public class SuchAccountExistsException : Exception
    {
        public SuchAccountExistsException()
        {

        }
        public SuchAccountExistsException(string message)
            : base(message)
        {

        }
        public SuchAccountExistsException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }

    public class AccountNotFoundException : Exception 
    {
        public AccountNotFoundException()
        {

        }
        public AccountNotFoundException(string message)
            : base(message)
        {

        }
        public AccountNotFoundException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}