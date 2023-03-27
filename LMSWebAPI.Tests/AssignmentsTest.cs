using Microsoft.AspNetCore.Mvc;
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
    public class AssignmentsTest
    {
        private AssignmentsController _controller;

        [Fact]
        public void Init()
        {
            _controller = new AssignmentsController();
        }

        [Fact]
        public void GetAssignmentById_ReturnsAssignment()
        {
            int assignmentId = 1;
            Assignment expectedAssignment = new Assignment { ModuleId = 1, Id = 1, Name = "M01 - Assignment 1", Grade = 90, DueDate = DateTime.Now };

            _controller.AddAssignment(expectedAssignment);

            Assignment actualAssignment = _controller.GetAssignment(assignmentId);

            Assert.Equal(actualAssignment.ModuleId,expectedAssignment.ModuleId);
            Assert.Equal(actualAssignment.Id, expectedAssignment.Id);
            Assert.Equal(actualAssignment.Name, expectedAssignment.Name);
            Assert.Equal(actualAssignment.Grade, expectedAssignment.Grade);
        }
    }
}
