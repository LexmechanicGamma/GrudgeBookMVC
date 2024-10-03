using Microsoft.EntityFrameworkCore;

namespace GrudgeBookMvc.src.Model.Postgres.Migration 
{ 
    public class UserData
    {
        public string Email { get; set; } = null!;
        public string Id { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public string SaltedPassword { get; set; } = null!;
    }   
}