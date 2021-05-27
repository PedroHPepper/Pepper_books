using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pepper_Books.Models.Entities
{
    /// <summary>
    /// Classe Venda. Aqui contem as informações necessárias para inserir no banco de dados
    /// </summary>
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public double TotalAmountPaid { get; set; }


        // Coleção da classe filha que permite incluir informações de venda referente à venda
        [ForeignKey("FKidSale")]
        public virtual ICollection<BookSale> BookSales { get; set; }
    }
}
