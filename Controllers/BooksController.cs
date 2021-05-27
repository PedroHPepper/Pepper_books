using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pepper_Books.Database;
using Pepper_Books.Models.Entities;

namespace Pepper_Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna os livros do banco de dados
        /// </summary>
        /// <returns>retorna uma lista dos livros</returns>
        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBook()
        {
            return await _context.Book.ToListAsync();
        }

        /// <summary>
        /// Usando um id, retorna o item específico contendo informações de um livro
        /// </summary>
        /// <param name="id">id do livro</param>
        /// <returns>Retorna um item do livro ou, caso não for encontrado, retorna um not found</returns>
        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Book.FindAsync(id);

            if (book == null)
            {
                //se não for encontrado, retorna not found.
                return NotFound();
            }

            return book;
        }

        /// <summary>
        /// atualiza um item de livro
        /// </summary>
        /// <param name="id">id do livro</param>
        /// <param name="book">informações do livro a serem atualizadas</param>
        /// <returns>se for encontrado o livro, retorna nocontent, se não retorna um notfound. Se o id estiver errado, retorna badrequest</returns>
        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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
        /// adiciona um item de livro
        /// </summary>
        /// <param name="book">Livro a ser adicionado</param>
        /// <returns>retorna o item inserido na BD</returns>
        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _context.Book.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        /// <summary>
        /// exclui do um livro do BD usando o ID dele
        /// </summary>
        /// <param name="id">id do livro a ser excluído</param>
        /// <returns>retorna um campo vazio</returns>
        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Verifica se o livro existe usando uma Id
        /// </summary>
        /// <param name="id">id a ser pesquisada</param>
        /// <returns>Retorna um booleano. Se existir um liro com id, retorna true, se não retorna false</returns>
        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
