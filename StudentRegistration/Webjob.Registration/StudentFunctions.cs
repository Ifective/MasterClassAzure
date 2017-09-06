using System;
using System.IO;
using Data;
using Logic;
using Logic.Services;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Webjob.Registration
{
    public class StudentFunctions
    {
        private readonly IStudentService _studentService;
        private readonly ISearchLogic _searchLogic;

        public StudentFunctions(IStudentService studentService, ISearchLogic searchLogic)
        {
            _studentService = studentService;
            _searchLogic = searchLogic;
        }

        public async Task ProcessQueueMessage([QueueTrigger("queue")] Logic.Model.Student student, TextWriter log)
        {
            log.WriteLine($"Saving Student: {student.Name}");
            var savedStudent = await _studentService.SaveStudent(student);
            await _searchLogic.AddStudentToSearchIndex(savedStudent);
        }
    }
}
