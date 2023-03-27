using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private static List<Module> _modules = new List<Module>
        {
            new Module { Id = 1, CourseId = 1, Name = "Introduction to C#" },
            new Module { Id = 2, CourseId = 1, Name = "Control Flow and Loops" },
            new Module { Id = 3, CourseId = 1, Name = "Arrays and Collections" },
            new Module { Id = 4, CourseId = 2, Name = "Introduction to Databases" },
            new Module { Id = 5, CourseId = 2, Name = "Relational Databases" },
            new Module { Id = 6, CourseId = 2, Name = "Querying Data" },
            new Module { Id = 7, CourseId = 3, Name = "Introduction to HTML and CSS" },
            new Module { Id = 8, CourseId = 3, Name = "JavaScript Fundamentals" },
            new Module { Id = 9, CourseId = 3, Name = "Building Responsive Layouts" }
        };

        [HttpGet]
        public IEnumerable<Module> GetAllModules()
        {
            return _modules;
        }


        // GET: Get module by Id
        [HttpGet("{id}")]
        public Module GetModule(int id)
        {
            return _modules.Find(module => module.Id == id);

            
        }


        // PUT: Update Module
        [HttpPut("{id}")]
        public IEnumerable<Module> PutModule(int oldId, Module module)
        {
            int index = _modules.FindIndex(course => course.Id == oldId);
            _modules[index] = module;
            return _modules;
        }

        // POST: Add Module
        [HttpPost]
        public IEnumerable<Module> PostModule(Module module)
        {
            _modules.Add(module);
            return _modules;
        }

        // DELETE: Delete Module
        [HttpDelete("{id}")]
        public IEnumerable<Module> DeleteModule(int id)
        {
            _modules.RemoveAll(module => module.Id == id);
            return _modules;
        }
    }
}
