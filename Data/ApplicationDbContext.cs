using ChubAPI.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace ChubAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> User{ get; set; }
        public DbSet<Chatroom> Chatroom { get; set; }
        public DbSet<ChatroomUsers> ChatroomUsers { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<MessageReads> MessageReads { get; set; }
    }
}
