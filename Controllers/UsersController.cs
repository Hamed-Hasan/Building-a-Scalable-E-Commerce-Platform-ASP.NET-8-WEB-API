using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EsapApi.Models;

namespace EsapApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public UsersController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, Users user)
        {
            if (id != user.userId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users user, int productId, decimal newPrice)
        {
            // Start a transaction
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Add the new user
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Retrieve the product to be updated
                var productToUpdate = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);

                // Check if product exists
                if (productToUpdate == null)
                {
                    // Rollback transaction and return error if product not found
                    await transaction.RollbackAsync();
                    return NotFound($"Product with ID {productId} not found.");
                }

                // Update the product's price
                productToUpdate.Price = newPrice;
                _context.Products.Update(productToUpdate);
                await _context.SaveChangesAsync();

                // Commit transaction if all operations succeed
                await transaction.CommitAsync();

                // Return the created user
                return CreatedAtAction(nameof(GetUsers), new { id = user.userId }, user);
            }
            catch (Exception ex)
            {
                // Rollback transaction if any operation fails
                await transaction.RollbackAsync();
                // Log the exception (logging not shown here)
                // Return a 500 error
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.userId == id);
        }
    }
}
