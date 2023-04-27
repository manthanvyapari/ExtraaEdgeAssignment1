using ExtraaEdgeAssignment1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExtraaEdgeAssignment1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostomerController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public CostomerController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        // GET: api/customers
        [HttpGet]
        [Route("GetAllCustomer")]

        public async Task<ActionResult<IEnumerable<Costomer>>> GetAllCustomers()
        {
            if (_dbContext.customers == null)
                return NotFound();

            return await _dbContext.customers.ToListAsync();
        }
        [HttpGet]
        [Route("GetCustomer/{id}")]

        public async Task<ActionResult<Costomer>> GetCustomerById(int id)
        {
            if (_dbContext.customers == null)
                return NotFound();

            var customer = await _dbContext.customers.FindAsync(id);

            if (customer == null)
                return NotFound();

            return customer;
        }
        [HttpPost]
        [Route("Add")]

        public async Task<ActionResult<Costomer>> AddCustomer(Costomer customer)
        {
            _dbContext.customers.Add(customer);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, customer);
        }
        [HttpPut]
        [Route("Update/{id}")]

        public async Task<IActionResult> UpdateCustomer(int id, Costomer customer)
        {
            if (id != customer.CustomerId)
                return BadRequest();

            _dbContext.Entry(customer).State = EntityState.Modified;


            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        private bool CustomerExists(long id)
        {
            return (_dbContext.customers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }


        [HttpDelete]
        [Route("Delete/{id}")]

        public async Task<ActionResult> DeleteCustomer(int id)
        {
            if (_dbContext == null)
                return NotFound();

            var customer = await _dbContext.customers.FindAsync(id);
            if (customer == null)
                return NotFound();

            _dbContext.customers.Remove(customer);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
