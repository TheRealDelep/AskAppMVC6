using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using System;
using System.Threading;

namespace AskApp.DAL
{
    public class AskAppContext : DbContext
    {
        public AskAppContext()
        {
        }

        public AskAppContext(DbContextOptions<AskAppContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder is null)
                throw new ArgumentNullException(nameof(optionsBuilder));

            optionsBuilder.UseSqlite(@"Data Source=AskAppDB.db;");
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<MessageEntity> Messages { get; set; }
        public DbSet<ThreadEntity> Threads { get; set; }
    }
}