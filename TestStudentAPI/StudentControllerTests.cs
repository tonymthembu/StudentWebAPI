using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentWebAPI.Controllers;
using StudentWebAPI.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentWebAPI.Models;

namespace TestStudentAPI
{
    [TestClass]
    public class StudentControllerTests
    {
        private AppDbContext _context;
        private StudentsController _controller;


        [TestInitialize]
        public void Initialize()
        {
            // Set up an in-memory database for testing
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "StudentApiDemo")
                .Options;

            _context = new AppDbContext(options);
            _controller = new StudentsController(_context);
        }

        [TestMethod]
        public async Task GetStudents_ReturnsAllStudents()
        {
            // Arrange
            var students = new List<Student>
            {
                new Student { Id = 1, Name = "Tony", Surname = "Mthembu", Email = "tonym@sts.com", Gender = "male", Grade = 1 },
                new Student { Id = 2, Name = "Nkazi", Surname = "Mthembu", Email = "nkazi@sts.com", Gender = "male", Grade = 3 },
                new Student { Id = 2, Name = "Kim", Surname = "Mthembu", Email = "kim@sts.com", Gender = "female", Grade = 5 },
            };
            _context.AddRange(students);
            _context.SaveChanges();

            // Act
            var result = await _controller.GetStudents();

            // Assert
            var okResult = result.Result as OkObjectResult;
            var studentItems = okResult.Value as IEnumerable<Student>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsNotNull(studentItems);
            Assert.AreEqual(3, studentItems.Count());
        }

        [TestMethod]
        public async Task GetStudent_ReturnsSingleStudent()
        {
            // Arrange
            var student = new Student { Id = 1, Name = "Tony", Surname = "Mthembu", Email = "tonym@sts.com", Gender = "male", Grade = 1 };
            _context.Add(student);
            _context.SaveChanges();

            // Act
            var result = await _controller.GetStudent(1);

            // Assert
            var okResult = result.Result as OkObjectResult;
            var studentItem = okResult.Value as Student;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsNotNull(studentItem);
            Assert.AreEqual(1, studentItem.Id);
        }
    }
}
