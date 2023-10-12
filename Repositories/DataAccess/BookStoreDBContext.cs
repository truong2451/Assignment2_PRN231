using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrinhHuuTruong.eBookStore.Repositories.Entity_Model;

namespace TrinhHuuTruong.eBookStore.Repositories.DataAccess
{
    public class BookStoreDBContext : DbContext
    {
        public BookStoreDBContext() { }

        public virtual DbSet<Author> Authors { get; set; }  
        public virtual DbSet<Book> Books { get; set; }  
        public virtual DbSet<BookAuthor> BookAuthors { get; set; }  
        public virtual DbSet<Publisher> Publishers { get; set; }  
        public virtual DbSet<Role> Roles { get; set; }  
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            //optionsBuilder.UseSqlServer(config["ConnectionStrings:DefaultConnection"]);
            optionsBuilder.UseSqlServer("server=(local);uid=sa;pwd=1;database=BookStoreDB;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>()
                .HasKey(x => new { x.AuthorId, x.BookId });
        }

    }
}
