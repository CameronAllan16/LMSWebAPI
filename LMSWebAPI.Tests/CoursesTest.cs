using Microsoft.AspNetCore.Routing;
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
        private CoursesController _controller;

        [Fact]
        public void Init()
        {
            _controller = new CoursesController();
        }

        [Fact]
        public void GetAllCourses_ReturnsAllCourses()
        {
            var result = _controller.GetAllCourses();

            Assert.IsType<IEnumerable<Course>>(result);
            Assert.Equal(result.Count(), 3);
        }


        [Fact]
        public void CreateCourse_ValidNameAndId_CourseCreated()
        {

            // Arrange
            var name = "Introduction to Programming";
            var id = 1;

            // Act
            var course = new Course { Id = id, Name = name };

            // Assert
            Assert.Equal(name, course.Name);
            Assert.Equal(id, course.Id);
        }
    }
}
