using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private static List<Course> _courses = new List<Course>
        {
            new Course { Id = 1, Name = "Introduction to Programming" },
            new Course { Id = 2, Name = "Database Management" },
            new Course { Id = 3, Name = "Web Development" }
        };

        [HttpGet]
        public IEnumerable<Course> GetAllCourses()
        {
            return _courses;
        }


        // GET: Get course by ID
        [HttpGet("{id}")]
        public Course GetCourse(int id)
        {
            return _courses.Find(course => course.Id == id);
        }


        // PUT: Update Course
        [HttpPut("{id}")]
        public IEnumerable<Course> UpdateCourse(int oldId, Course course)
        {
            int index = _courses.FindIndex(course => course.Id == oldId);
            _courses[index] = course;
            return _courses;
        }

        // POST: Add Course
        [HttpPost]
        public IEnumerable<Course> AddCourse(Course course)
        {
            _courses.Add(course);
            return _courses;
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public IEnumerable<Course> DeleteCourse(int id)
        {
            _courses.RemoveAll(course => course.Id == id);
            return _courses;
        }
    }
}
