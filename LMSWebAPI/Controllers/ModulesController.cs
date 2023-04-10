using LMSWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly LMSContext _context; 

        public ModulesController(LMSContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Module>>> GetAllModules()
        {
            return await _context.Modules.ToListAsync();
        }


        // GET: Get module by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Module>> GetModule(int id)
        {
           var module = await _context.Modules.FindAsync(id);

            if (module == null)
            {
                return NotFound();
            }

            return module;

        }

        // GET: Get Assignments In a Module
        [HttpGet("{id}/assignments")]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetModuleAssignments(int id)
        {
            var module = await _context.Modules.FindAsync(id);

            if (module == null)
            {
                return NotFound();
            }

            var assignments = await _context.Assignments
                .Where(a => a.Id == id)
                .ToListAsync();

            return assignments;
        }


        // PUT: Update Module
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModule(int oldId, Module module)
        {
            if (oldId != module.Id) 
            {
                return BadRequest();
            }

            _context.Entry(module).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Modules.Any(m => m.Id == oldId))
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

        // POST: Add Module
        [HttpPost]
        public async Task<ActionResult<Module>> PostModule(Module module)
        {
            _context.Modules.Add(module);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetModule), new { id = module.Id }, module);
        }

        // DELETE: Delete Module
        [HttpDelete("{id}")]
        public async Task<ActionResult<Module>> DeleteModule(int id)
        {
           var module = await _context.Modules.FindAsync(id);

           if (module == null)
            {
                return NotFound();
            }

            _context.Modules.Remove(module);
            await _context.SaveChangesAsync();

            return module;
        }
    }
}
