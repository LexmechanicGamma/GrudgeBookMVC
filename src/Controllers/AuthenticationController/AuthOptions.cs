using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GrudgeBookMvc.src.Controllers.AuthenticationController
{
    public static class AuthOptions
    {
        public static string ISSUER = "Karak"; /////
        public static string AUDIENCE = "Dwarves"; /////
        public static string KEY = Environment.GetEnvironmentVariable("DwarfBook__AuthTokenKey");
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}