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
    public class ModulesTests
    {

        private ModulesController _controller;


        public void Setup()
        {
            _controller = new ModulesController();
        }

        [Fact]
        public void GetModule_ReturnsModule()
        {
            int moduleId = 2;
            Module expectedModule = new Module { Id = 2, CourseId = 1, Name = "Control Flow and Loops" };

            Module actualModule = _controller.GetModule(1);

            Assert.Equal(actualModule.CourseId, expectedModule.CourseId);
            Assert.Equal(actualModule.Id, expectedModule.Id);
            Assert.Equal(actualModule.Name, expectedModule.Name);
        }
    }
}
