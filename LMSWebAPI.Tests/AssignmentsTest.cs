using LMSWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using WebApi.Controllers;
using WebApi.Models;

namespace LMSWebAPI.Tests
{
    public class AssignmentsTest
    {
        [Fact]
        public void GetAssignmentsByModuleId_ReturnsAssignments()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<LMSContext>()
                .UseInMemoryDatabase(databaseName: "GetAssignmentsByModuleId_ReturnsAssignments")
                .Options;

            var moduleId = 1;

            using (var context = new LMSContext(options))
            {
                context.Assignments.AddRange(
                    new List<Assignment>
                    {
                        new Assignment { Id = 1, Name = "Assignment 1", ModuleId = moduleId },
                        new Assignment { Id = 2, Name = "Assignment 2", ModuleId = moduleId },
                        new Assignment { Id = 3, Name = "Assignment 3", ModuleId = 2 }
                    });
                context.SaveChanges();
            }

            using (var context = new LMSContext(options))
            {
                var controller = new AssignmentsController(context);

                // Act
                var result = controller.GetAssignment(moduleId);

                // Assert
                Assert.NotNull(result);

                var assignments = Assert.IsAssignableFrom<IEnumerable<Assignment>>(result.Result);
                Assert.Equal(2, assignments.Count());
                Assert.Equal("Assignment 1", assignments.ElementAt(0).Name);
                Assert.Equal("Assignment 2", assignments.ElementAt(1).Name);
            }
        }

        [Fact]
        public void CreateAssignment_ReturnsCreatedAssignment()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<LMSContext>()
                .UseInMemoryDatabase(databaseName: "CreateAssignment_ReturnsCreatedAssignment")
                .Options;

            var moduleId = 1;
            var id = 1;
            var grade = 97;

            var newAssignment = new Assignment
            {
                Id = id,
                Name = "New Assignment",
                Grade = grade,
                DueDate = new System.DateTime(2023, 4, 30),
                ModuleId = moduleId
            };

            using (var context = new LMSContext(options))
            {
                var controller = new AssignmentsController(context);

                // Act
                var result = controller.AddAssignment(newAssignment);

                // Assert
                Assert.NotNull(result);

                var createdAssignment = Assert.IsAssignableFrom<Assignment>(result.Result);
                Assert.Equal(newAssignment.Name, createdAssignment.Name);
                Assert.Equal(newAssignment.DueDate, createdAssignment.DueDate);
                Assert.Equal(newAssignment.ModuleId, createdAssignment.ModuleId);

                var assignmentsInDb = context.Assignments.ToList();
                Assert.Single(assignmentsInDb);
                Assert.Equal(newAssignment.Name, assignmentsInDb[0].Name);
                Assert.Equal(newAssignment.DueDate, assignmentsInDb[0].DueDate);
                Assert.Equal(newAssignment.ModuleId, assignmentsInDb[0].ModuleId);
            }
        }
}
