using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pepper_Books.Models.Entities
{
    /// <summary>
    /// Classe LivroVenda. Aqui contem as informações necessárias para inserir no banco de dados
    /// </summary>
    public class BookSale
    {
        [Key]
        public int Id { get; set; }
        public double AmountPaid { get; set; }
        [Required]
        public int Quantity { get; set; }

        // chaves estrangeiras
        [ForeignKey("Book")]
        public int FKidBook { get; set; }
        [ForeignKey("Sale")]
        public int FKidSale { get; set; }


        // classes pai
        public virtual Book Book { get; set; }
        public virtual Sale Sale { get; set; }
    }
}
