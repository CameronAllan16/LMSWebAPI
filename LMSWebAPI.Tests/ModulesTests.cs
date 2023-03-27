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


        public void Init()
        {
            _controller = new ModulesController();
        }

        [Fact]
        public void GetModuleMethod_ReturnsModule()
        {
            int moduleId = 2;
            Module expectedModule = new Module { Id = 2, CourseId = 1, Name = "Control Flow and Loops" };

            Module actualModule = _controller.GetModule(1);

            Assert.Equal(actualModule.CourseId, expectedModule.CourseId);
            Assert.Equal(actualModule.Id, expectedModule.Id);
            Assert.Equal(actualModule.Name, expectedModule.Name);
        }

        [Fact]
        public void AddModuleMethod_AddsModuleToList()
        {
            var moduleToAdd = new Module { CourseId = 1, Id = 4, Name = "Circles" };

            var result = _controller.PostModule(moduleToAdd);

            Assert.Contains(moduleToAdd, result);
            Assert.Equal( 4 , result.Count());
        }

        [Fact]
        public void DeleteModuleMethod_RemovesModule()
        {
            var moduleId = 3;

            var result = _controller.DeleteModule(moduleId);

            Assert.Equal( null , result.FirstOrDefault(module => module.Id == moduleId));
            Assert.Equal( 2, result.Count());
        }
    }
}
