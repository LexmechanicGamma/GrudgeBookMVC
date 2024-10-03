using Microsoft.EntityFrameworkCore;

namespace GrudgeBookMvc.src.Model.Postgres.Authentication 
{ 
    public class UserData
    {
        public string Email { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Salt { get; set; }
        public string SaltedPassword { get; set; }

        public UserData(
            string Email,
            string Id,
            string UserName,
            string Salt,
            string SaltedPassword
            ) 
        {
            if (Email != null) { this.Email = Email; }
            this.Id = Id;
            this.UserName = UserName;
            this.Salt = Salt;
            this.SaltedPassword = SaltedPassword;
        }
    }   

    public class UserDataContext : DbContext
    {
        public DbSet<UserData> usersData { get; set; }
        public UserDataContext(DbContextOptions<UserDataContext> options)
            : base(options)
        {
            Database.Migrate(); 
        }
    }
}
