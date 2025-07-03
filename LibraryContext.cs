using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMgmt
{
    internal class LibraryContext : DbContext
    {
        string connectionstring = @"Server=(localdb)\MSSQLLocalDB;Database=DB1;Trusted_Connection=True;";
        internal DbSet<Book> Books {  get; set; }
        internal DbSet<Author> Authors { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionstring);
        }
    }
}
