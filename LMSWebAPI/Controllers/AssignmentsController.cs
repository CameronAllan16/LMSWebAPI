using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        List<Assignment> _assignments = new List<Assignment>
        {
            new Assignment { ModuleId = 1, Id = 1, Name = "M01 - Assignment 1", Grade = 90, DueDate = DateTime.Now},
            new Assignment { ModuleId = 2, Id = 2, Name = "M02 - Assignment 2", Grade = 100, DueDate = DateTime.Now},
            new Assignment { ModuleId = 2, Id = 3, Name = "M02 - Quiz 1", Grade = 99, DueDate = DateTime.Now},
            new Assignment { ModuleId = 2, Id = 4, Name = "M02 - Assignment 3", Grade = 85, DueDate = DateTime.Now},
            new Assignment { ModuleId = 3, Id = 5, Name = "M03 - Quiz 2", Grade = 89, DueDate = DateTime.Now}
        };
       
        // Get All Assignments
        [HttpGet]
        public IEnumerable<Assignment> GetAssignments()
        {
            return _assignments;
        }


        //Get Assignment by ID
        [HttpGet("{id}")]
        public Assignment GetAssignment(int id)
        {
            var assignment = _assignments.Find(assignment => assignment.Id == id);

            return assignment;
        }

        //Update Assignment
        [HttpPut("{id}")]
        public IEnumerable<Assignment> UpdateAssignment(int oldId, Assignment newAssignment)
        {
            int index = _assignments.FindIndex(assignment => assignment.Id == oldId);


            _assignments[index] = newAssignment;
            return _assignments;
        }

        // Add Assignment
        [HttpPost]
        public IEnumerable<Assignment> AddAssignment(Assignment assignment)
        {
            _assignments.Add(assignment);
            return _assignments;
        }

        // Delete Assignment
        [HttpDelete("{id}")]
        public IEnumerable<Assignment> DeleteAssignment(int id)
        {
           
            _assignments.RemoveAll(assignment => assignment.Id == id);

            return _assignments;
        }
    }
}
