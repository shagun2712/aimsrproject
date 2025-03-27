using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InteriorDesignWebsite.Models;
using Microsoft.EntityFrameworkCore;
using InteriorDesignWebsite.Data;

namespace InteriorDesignWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactFormController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContactFormController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ContactForm
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactForm>>> Get()
        {
            var contactForms = await _context.ContactForms.ToListAsync();
            return Ok(contactForms);
        }

        // GET: api/ContactForm/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactForm>> Get(int id)
        {
            var contact = await _context.ContactForms.FindAsync(id);
            if (contact == null)
                return NotFound(new { message = "Contact form not found" });

            return Ok(contact);
        }

        // POST: api/ContactForm
        [HttpPost]
        public async Task<ActionResult<ContactForm>> Post([FromBody] ContactForm contactForm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Add the new contact form to the database
            _context.ContactForms.Add(contactForm);
            await _context.SaveChangesAsync(); // Save changes to the database

            // Return the created contact form with its id
            return CreatedAtAction(nameof(Get), new { id = contactForm.Id }, contactForm);
        }

        // PUT: api/ContactForm/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ContactForm updatedContact)
        {
            var contact = await _context.ContactForms.FindAsync(id);
            if (contact == null)
                return NotFound(new { message = "Contact form not found" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Update the contact form with the new data
            contact.FullName = updatedContact.FullName;
            contact.EmailAddress = updatedContact.EmailAddress;
            contact.PhoneNumber = updatedContact.PhoneNumber;
            contact.SpaceToDesign = updatedContact.SpaceToDesign;
            contact.EstimatedBudget = updatedContact.EstimatedBudget;
            contact.DesignStyle = updatedContact.DesignStyle;
            contact.ProjectTimeline = updatedContact.ProjectTimeline;
            contact.SpecialRequirements = updatedContact.SpecialRequirements;
            contact.HeardAboutUs = updatedContact.HeardAboutUs;
            contact.Message = updatedContact.Message;

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return NoContent status as the update is successful
            return NoContent();
        }

        // DELETE: api/ContactForm/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _context.ContactForms.FindAsync(id);
            if (contact == null)
                return NotFound(new { message = "Contact form not found" });

            // Remove the contact form from the database
            _context.ContactForms.Remove(contact);
            await _context.SaveChangesAsync(); // Save changes to the database

            // Return NoContent status as the deletion is successful
            return NoContent();
        }
    }
}
