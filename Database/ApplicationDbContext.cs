using Microsoft.EntityFrameworkCore;
using Pepper_Books.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pepper_Books.Database
{
    /// <summary>
    /// Classe que configura o banco de dados
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        // classe usa injeção de dependência pela classe startup
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //itens que representam as tabelas do banco e permitem setar interações com elas.
        public DbSet<Book> Book { get; set; }
        public DbSet<BookSale> BookSale { get; set; }
        public DbSet<Sale> Sale { get; set; }
    }
}
