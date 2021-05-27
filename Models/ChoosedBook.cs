using Pepper_Books.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pepper_Books.Models
{
    /// <summary>
    /// Item de escolha de livro. Contendo preço, id do livro e quantidade
    /// </summary>
    public class ChoosedBook
    {
        public int BookID { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
