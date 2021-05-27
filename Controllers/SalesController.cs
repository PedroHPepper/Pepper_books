using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pepper_Books.Database;
using Pepper_Books.Models;
using Pepper_Books.Models.Entities;

namespace Pepper_Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todas as vendas, juntamente com quais livros foram escolhidos juntamente com a quantidade
        /// </summary>
        /// <returns>Retorna uma lista de vendas</returns>
        // GET: api/Sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSale()
        {
            return await _context.Sale.AsNoTracking().Include(e => e.BookSales).ToListAsync();
        }

        /// <summary>
        /// Retorna a venda, juntamente com quais livros foram escolhidos juntamente com a quantidade
        /// </summary>
        /// <param name="id">Id da venda</param>
        /// <returns>Se não for encontrado nada, retorna um not found</returns>
        // GET: api/Sales/5
        [HttpGet("{id}")]
        public ActionResult<Sale> GetSale(int id)
        {
            var sale = _context.Sale.AsNoTracking().Where(e => e.Id == id).Include(e => e.BookSales).FirstOrDefault();

            if (sale == null)
            {
                return NotFound();
            }

            return sale;
        }

        /// <summary>
        /// Atualiza uma venda
        /// </summary>
        /// <param name="id">Id da venda</param>
        /// <param name="sale">Tabela da venda com os campos novos</param>
        /// <returns></returns>
        // PUT: api/Sales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSale(int id, Sale sale)
        {
            if (id != sale.Id)
            {
                return BadRequest();
            }

            _context.Entry(sale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Adiciona uma venda com uma lista de livros e sua quantidade
        /// </summary>
        /// <param name="BookList"></param>
        /// <returns></returns>
        // POST: api/Sales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //Choosed book contem o id do livro, o valor pago e a quantidade comprada desse livro
        public async Task<ActionResult<Sale>> PostSale(List<ChoosedBook> BookList)
        {
            //cria um item de venda somando o preço com a quantidade
            Sale sale = new Sale()
            {
                TotalAmountPaid = BookList.Sum(e => e.Price * e.Quantity)
            };
            _context.Sale.Add(sale);
            await _context.SaveChangesAsync();

            List<BookSale> bookSales = new List<BookSale>();
            foreach(ChoosedBook ChoosedBook in BookList)
            {
                BookSale bookSale = new BookSale()
                {
                    AmountPaid = ChoosedBook.Price,
                    FKidBook = ChoosedBook.BookID,
                    FKidSale = sale.Id,
                    Quantity = ChoosedBook.Quantity
                };
                _context.BookSale.Add(bookSale);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Exclui um item de venda usando o id da venda, excluindo também seus filhos
        /// </summary>
        /// <param name="id">id venda</param>
        /// <returns></returns>
        // DELETE: api/Sales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            var sale = await _context.Sale.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            List<BookSale> bookSales = await _context.BookSale.Where(e => e.FKidSale == sale.Id).ToListAsync();
            foreach(BookSale bookSale in bookSales)
            {
                _context.BookSale.Remove(bookSale);
            }

            _context.Sale.Remove(sale);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Verifica se a venda existe usando o id
        /// </summary>
        /// <param name="id">id venda</param>
        /// <returns></returns>
        private bool SaleExists(int id)
        {
            return _context.Sale.Any(e => e.Id == id);
        }
    }
}
