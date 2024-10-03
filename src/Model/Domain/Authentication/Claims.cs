using System.Security.Claims;
namespace GrudgeBookMvc.src.Model.Domain.Authentication
{
    public static class Claims
    {
        public static List<Claim> ToClaims(Postgres.Authentication.UserData userData)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userData.Id),
                new Claim(ClaimTypes.Name, userData.UserName)
            };
            return claims;
        }
    }
}
