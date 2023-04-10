using LMSWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {

        private readonly LMSContext _context;

        public AssignmentsController(LMSContext context)
        {
            _context = context;
        }
       
        // Get All Assignments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetAssignments()
        {
            return await _context.Assignments.ToListAsync();
        }


        //Get Assignment by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Assignment>> GetAssignment(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);

            if (assignment == null)
            {
                return NotFound();
            }

            return assignment;
        }

        //Update Assignment
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssignment(int oldId, Assignment newAssignment)
        {
            if (oldId != newAssignment.Id)
            {
                return BadRequest();
            }

            var moduleExists = await _context.Modules.AnyAsync(m => m.Id == newAssignment.Id);
            if (!moduleExists)
            {
                return BadRequest("Invalid ModuleId");
            }

            _context.Entry(newAssignment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Assignments.Any(a => a.Id == oldId)) 
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

        // Add Assignment
        [HttpPost]
        public async Task<ActionResult<Assignment>> AddAssignment(Assignment assignment)
        {
            _context.Assignments.Add(assignment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAssignment), new { id = assignment.Id }, assignment);
        }

        // Delete Assignment
        [HttpDelete("{id}")]
        public async Task<ActionResult<Assignment>> DeleteAssignment(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);

            if(assignment == null)
            {
                return NotFound();
            }

            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();

            return assignment;
        }
    }
}
