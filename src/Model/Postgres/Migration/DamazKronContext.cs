using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GrudgeBookMvc.src.Model.Postgres.Migration;

public partial class DamazKronContext : DbContext
{
    public virtual DbSet<Authentication.UserData> UsersData { get; set; }
    public virtual DbSet<Book.Grudge> Grudges { get; set; }

    public DamazKronContext()
    {
    }

    public DamazKronContext(DbContextOptions<DamazKronContext> options)
        : base(options)
    {
        Database.Migrate();
    }
}
