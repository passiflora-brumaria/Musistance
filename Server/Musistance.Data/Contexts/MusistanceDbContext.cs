using Microsoft.EntityFrameworkCore;
using Musistance.Data.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musistance.Data.Contexts
{
    /// <summary>
    /// Database data persistence (Repositories + Unit Of Work).
    /// </summary>
    public class MusistanceDbContext: DbContext
    {
        private readonly string _conn;
        public MusistanceDbContext (string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Musistance;Trusted_Connection=True;MultipleActiveResultSets=true")
        {
            _conn = connectionString;
        }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_conn);
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserProfile>().HasQueryFilter(x => !x.DeletedAt.HasValue);
        }

        public DbSet<UserProfile> Profiles { get; set; }
    }
}
