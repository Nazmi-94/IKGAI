using IKGAi.Models;
using Microsoft.EntityFrameworkCore;

namespace IKGAi.Models
{
    public class DB : DbContext
    {
        //1. Define the connection string
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=NAZMI-A2;Database=Ikgai;User ID=sa;Password=sa;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        //}

        public DB(DbContextOptions<DB> context) : base(context) 
        {
        }


        //2. Define the models that will be tables in the database
        public DbSet<Career> Career { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserProfile> Profiles { get; set; }
        public DbSet<Comment> Comment { get; set; } 

        
    }
}
