using Microsoft.EntityFrameworkCore;
using ContactManagementApi.Database.Models;

namespace ContactManagementApi.Database.Context
{
    public class ApplicationDbContext : DbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<PlaceOfResidence> PlacesOfResidence { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasMany(u => u.People)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()                
                .Property(u => u.Role)
                .HasConversion<int>();

            modelBuilder.Entity<Person>()
                .HasOne(x => x.PlaceOfResidence)
                .WithOne(x => x.Person)
                .HasForeignKey<PlaceOfResidence>(pr => pr.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<User>()
        //        .HasMany(u => u.People)
        //        .WithOne(p => p.User)
        //        .HasForeignKey(p => p.UserId)
        //        .OnDelete(DeleteBehavior.Cascade);


        //}
    }
}