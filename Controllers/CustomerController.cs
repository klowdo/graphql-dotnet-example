using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using graphql_dotnet.Configuration.data;
using graphql_dotnet.Models.Requests;
using graphql_dotnet.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace graphql_dotnet.Controllers
{
    [Route("api/customers")]
    public class CustomerController: Controller
    {
        private readonly BigBadBankContext _context;

        public CustomerController(BigBadBankContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<CustomerResponse>), 200)]
        public async Task<IActionResult> GetCustomers(){
            var customers = await _context.Customers.ToListAsync();
            return Ok(customers.Select(ResponseFactory.CreateResponse));
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ProducesResponseType(typeof(CustomerResponse), 200)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<IActionResult> GetCustomer(Guid id){
            var customer = await _context.Customers.Include(x => x.Notes).FirstOrDefaultAsync(c => c.Id == id);
            if(customer is null) return NotFound();
            return Ok(ResponseFactory.CreateResponse(customer));
        }
       [HttpGet]
       [Route("{id:guid}/notes")]
       [ProducesResponseType(typeof(IEnumerable<NoteResponse>), 200)]
       [ProducesResponseType(typeof(void), 404)]
        public async Task<IActionResult> GetCustomerNotes(Guid id){
            var customer = await _context.Customers.Include(x => x.Notes).FirstOrDefaultAsync(c => c.Id == id);
            if(customer is null) return NotFound();
            return Ok(customer.Notes.Select(ResponseFactory.CreateResponse));
        }
        [ActionName("note")]
        [HttpGet]
        [Route("{id:guid}/notes/{noteid:guid}")]
        [ProducesResponseType(typeof(IEnumerable<NoteResponse>), 200)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<IActionResult> GetCustomerNote(Guid id, Guid noteId){
            var customer = await _context.Customers.Include(x => x.Notes).FirstOrDefaultAsync(c => c.Id == id);
            if(customer is null) return NotFound();
            var note = customer.Notes.FirstOrDefault(x => x.Id == noteId);
            if(note is null) return NotFound();
            return Ok(ResponseFactory.CreateResponse(note));
        }
        [HttpPost]
        [Route("{id:guid}/notes")]
        [ProducesResponseType(typeof(NoteResponse), 201)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<IActionResult> CreateCustomerNotes(Guid id,[FromBody] NewNoteRequest model){
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var customer = await _context.Customers.FindAsync(id);
            if(customer is null) return NotFound();
            var note = customer.AddNote(model.Text,model.CreatedBy);
            await _context.SaveChangesAsync();
            return CreatedAtAction("note",new {Id = id, noteId = note.Id }, note);
        }
        [HttpGet]
        [Route("{id:guid}/engagements")]
        [ProducesResponseType(typeof(EngagementResponse), 200)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<IActionResult> GetCustomerEngagements(Guid id){
            var customer = await _context.Customers.Include(x => x.Engagements).FirstOrDefaultAsync(c => c.Id == id);
            if(customer is null) return NotFound();
            return Ok(customer.Engagements.Select(ResponseFactory.CreateResponse));
        }
    }
}