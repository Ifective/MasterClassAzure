using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Logic.Model;
using Logic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using StudentRegistration.WebApp.Models;
using Microsoft.Extensions.Configuration;

namespace StudentRegistration.WebApp.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    public class StudentController : Controller
    {
        private readonly IQueueLogic _queueLogic;
        private readonly IStudentService _studentService;
        private readonly IConfiguration _configuration;
        private readonly ISearchLogic _searchLogic;

        public StudentController(IQueueLogic queueLogic, 
            IStudentService studentService,
            IConfiguration configuration,
            ISearchLogic searchLogic)
        {
            _queueLogic = queueLogic;
            _studentService = studentService;
            _configuration = configuration;
            _searchLogic = searchLogic;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _studentService.GetStudents();

            ViewBag.Environment = _configuration["Environment"] ?? "Unknown";

            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            await _queueLogic.AddStudent(student);
            return RedirectToAction("Index");
        }

        public IActionResult Search()
        {
            return View(new SearchViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Search(string query)
        {
            var students = await _searchLogic.SearchStudents(query);
            var viewModel = new SearchViewModel()
            {
                Students = students
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id) // Should be post in real!
        {
            await _studentService.DeleteStudent(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Error()
        {
            throw new Exception("Demo Exception oops :(");
        }
    }
}
