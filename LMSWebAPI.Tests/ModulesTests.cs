using LMSWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Controllers;
using WebApi.Models;

namespace LMSWebAPI.Tests
{
    public class ModulesTests
    {

        private DbContextOptionsBuilder<LMSContext> _optionsBuilder;

        public ModulesTests()
        {
            // Set up the in-memory database
            _optionsBuilder = new DbContextOptionsBuilder<LMSContext>()
                .UseInMemoryDatabase("TestDatabase");
        }

        [Fact]
        public async Task GetModuleById_ReturnsNotFoundResult_WhenModuleNotFound()
        {
            // Arrange
            using (var context = new LMSContext(_optionsBuilder.Options))
            {
                var controller = new ModulesController(context);

                // Act
                var result = await controller.GetModule(1);

                // Assert
                Assert.IsType<NotFoundResult>(result.Result);
            }
        }

        [Fact]
        public async Task GetModuleById_ReturnsModule_WhenModuleFound()
        {
            // Arrange
            using (var context = new LMSContext(_optionsBuilder.Options))
            {
                // Add a module to the in-memory database
                var module = new Module { Name = "Test Module" };
                context.Modules.Add(module);
                await context.SaveChangesAsync();

                var controller = new ModulesController(context);

                // Act
                var result = await controller.GetModule(module.Id);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result.Result);
                var returnedModule = Assert.IsType<Module>(okResult.Value);
                Assert.Equal(module.Id, returnedModule.Id);
                Assert.Equal(module.Name, returnedModule.Name);
            }
        }

        [Fact]
        public async Task GetModuleAssignments_ReturnsNotFoundResult_WhenModuleNotFound()
        {
            // Arrange
            using (var context = new LMSContext(_optionsBuilder.Options))
            {
                var controller = new ModulesController(context);

                // Act
                var result = await controller.GetModuleAssignments(1);

                // Assert
                Assert.IsType<NotFoundResult>(result.Result);
            }
        }

        [Fact]
        public async Task GetModuleAssignments_ReturnsAssignments_WhenModuleFound()
        {
            // Arrange
            using (var context = new LMSContext(_optionsBuilder.Options))
            {
                // Add a module and some assignments to the in-memory database
                var module = new Module { Name = "Test Module" };
                var assignment1 = new Assignment { Name = "Test Assignment 1", ModuleId = module.Id };
                var assignment2 = new Assignment { Name = "Test Assignment 2", ModuleId = module.Id };
                context.Modules.Add(module);
                context.Assignments.Add(assignment1);
                context.Assignments.Add(assignment2);
                await context.SaveChangesAsync();

                var controller = new ModulesController(context);

                // Act
                var result = await controller.GetModuleAssignments(module.Id);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result.Result);
                var returnedAssignments = Assert.IsAssignableFrom<IEnumerable<Assignment>>(okResult.Value);
                Assert.Equal(2, returnedAssignments.Count());
            }
        }
    }
}
