namespace GrudgeBookMvc.src.Views.Json.AuthenticationData
{
    public class JWToken
    {
        public string access_token { get; set; }
        public string username { get; set; }
        public JWToken(string accessToken, string username) 
        {
            this.access_token = accessToken;
            this.username = username;
        }
    }
}
