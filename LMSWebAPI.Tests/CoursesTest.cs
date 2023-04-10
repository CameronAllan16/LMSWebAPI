using LMSWebAPI.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Models;

namespace LMSWebAPI.Tests
{
    public class CoursesTest
    {
        [Fact]
        public void GetCourses_ReturnsAllCourses()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<LMSContext>()
                .UseInMemoryDatabase(databaseName: "GetCourses_ReturnsAllCourses")
                .Options;

            using (var context = new LMSContext(options))
            {
                context.Courses.AddRange(
                    new List<Course>
                    {
                        new Course { Id = 1, Name = "Course 1" },
                        new Course { Id = 2, Name = "Course 2" }
                    });
                context.SaveChanges();
            }

            using (var context = new LMSContext(options))
            {
                var controller = new CoursesController(context);

                // Act
                var result = controller.GetAllCourses();

                // Assert
                Assert.NotNull(result);

                var courses = Assert.IsAssignableFrom<IEnumerable<Course>>(result.Result);
                Assert.Equal(2, courses.Count());
                Assert.Equal("Course 1", courses.ElementAt(0).Name);
                Assert.Equal("Course 2", courses.ElementAt(1).Name);
            }
        }

        [Fact]
        public void GetCourseById_ReturnsCourse()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<LMSContext>()
                .UseInMemoryDatabase(databaseName: "GetCourseById_ReturnsCourse")
                .Options;

            var courseId = 1;

            using (var context = new LMSContext(options))
            {
                context.Courses.AddRange(
                    new List<Course>
                    {
                        new Course { Id = courseId, Name = "Course 1" },
                        new Course { Id = 2, Name = "Course 2" }
                    });
                context.SaveChanges();
            }

            using (var context = new LMSContext(options))
            {
                var controller = new CoursesController(context);

                // Act
                var result = controller.GetCourse(courseId);

                // Assert
                Assert.NotNull(result);

                var course = Assert.IsAssignableFrom<Course>(result.Result);
                Assert.Equal("Course 1", course.Name);
            }
        }
    }
}
