namespace GrudgeBookMvc.src.Controllers.AuthenticationController
{
    public class LoginInputsException : Exception
    {
        public LoginInputsException()
        {

        }
        public LoginInputsException(string message)
            : base(message)
        {

        }
        public LoginInputsException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}