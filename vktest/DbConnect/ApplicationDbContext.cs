using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Data;
using System.Reflection.Emit;
using vktest.DbConnect.TestData;
using vktest.Models;

namespace vktest.DbConnect
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserState> States { get; set; }
        public DbSet<UserGroup> Groups { get; set; }

        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=postgres");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(p => p.UserGroup)
                .WithMany(c => c.Users)
                .HasForeignKey(p => p.User_Group_Id);
            
            modelBuilder.Entity<User>()
                .HasOne(p => p.UserState)
                .WithMany(c => c.Users)
                .HasForeignKey(p => p.User_State_Id);

            modelBuilder.Entity<UserState>().HasData(new TestStatesData().GetData());
            modelBuilder.Entity<UserGroup>().HasData(new TestGroupData().GetData());
            modelBuilder.Entity<User>().HasData(new TestUserData().GetData());
            base.OnModelCreating(modelBuilder);
        }
    }
}
