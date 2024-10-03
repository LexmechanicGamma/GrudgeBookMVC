using System.Security.Cryptography;
using System.Text;

namespace GrudgeBookMvc.src.Model.Domain.Authentication
{
    public record class UserData
    {      
        public string Email { get; set; } = string.Empty;
        public string Id { get; init; } 
        public string Username { get; init; }

        private string _salt = RandomNumberGenerator.GetBytes(128 / 8).ToString();
        public string Salt
        {
            get { return _salt; }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            init { _password = Encryptor.ToSHA256(value, _salt); }
        }

        public UserData(
            string Email,
            string Username,
            string Password
            ) 
        {
            Id = Guid.NewGuid().ToString();
            if( Email != null ) { this.Email = Email; }
            this.Username = Username;
            this.Password = Password;
        }

    }

    internal static class Encryptor
    {
        internal static string ToSHA256(string str, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                str += salt;
                StringBuilder hash = new StringBuilder();
                byte[] hashArray = sha256.ComputeHash(
                    Encoding.UTF8.GetBytes(str));
                foreach (byte b in hashArray)
                {
                    hash.Append(b.ToString());
                }
                return hash.ToString();
            }
        }
    }
}