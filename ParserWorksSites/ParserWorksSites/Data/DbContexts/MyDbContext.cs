using Microsoft.EntityFrameworkCore;
using ParserWorksSites.Data.Models;

namespace ParserWorksSites.Data.DbContexts
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() { }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        { }

        public DbSet<Vacancy> Vacancy { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vacancy>()
                .Property(v => v.Title)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}