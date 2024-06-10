using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using northwind.MYSQL.Procedures;
using Northwind.Data;
using Northwind.Models;

namespace northwind.MYSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly NorthwindContext _context;
        private readonly NorthWindContextProcedures _contextProcedures;

        /// <summary>
        /// Contructor for Contoller class
        /// </summary>
        /// <param name="context"></param>
        public ProductsController(NorthwindContext context, NorthWindContextProcedures contextProcedures)
        {
            _context = context;
            _contextProcedures = contextProcedures;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'NorthwindContext.Products'  is null.");
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
        /// <summary>
        /// Retreive Supplier Information based on the product name
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        [HttpGet("GetSuplierFromProductName")]
        public async Task<Product?> GetSupplierProductName(string? productName)
        {
            return await _context.Products
                .Include(p => p.Supplier)
                .Where(p => p.ProductName == productName)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Calling View using EF Core
        /// When we scafold a Database it will create a model from the view
        /// Model : AlphabeticalListOfProduct
        /// AlphabeticalListOfProducts : DB Set that we going to get the data
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAlphabeticalListOfProducts")]
        public async Task<IEnumerable<AlphabeticalListOfProduct>> GetAlphabeticalListOfProducts()
        {
            return await _context.AlphabeticalListOfProducts.ToListAsync();
        }
        /// <summary>
        /// Calling Storeprocedures using EFcoere
        /// We need to create seperate Model and DBContexr class for call storedprocedures
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("GetCustOrderHistory")]
        public async Task<IEnumerable<CustomerOrderHistory>> GetCustOrderHistory(string customerId)
        {
            ///Pass Procedure name and parameter 
            return await _contextProcedures.CustOrderHistories
                .FromSqlRaw("call cust_order_history({0})", customerId)
                .ToListAsync();
        }

    }

}
