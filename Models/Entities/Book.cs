using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pepper_Books.Models.Entities
{
    /// <summary>
    /// Classe livro. Aqui contem as informações necessárias para inserir no banco de dados
    /// </summary>
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }


        // Coleção da classe filha que permite incluir informações de venda referente ao livro
        [ForeignKey("FKidBook")]
        public virtual ICollection<BookSale> BookSales { get; set; }
    }
}
