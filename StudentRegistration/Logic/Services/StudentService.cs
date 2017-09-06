using System.Collections.Generic;
using System.Linq;
using Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace Logic.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public StudentService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Logic.Model.Student> SaveStudent(Logic.Model.Student student)
        {
            var dbStudent = new Data.Student()
            {
                Name = student.Name,
                Email = student.Email
            };
            _applicationDbContext.Students.Add(dbStudent);

            await _applicationDbContext.SaveChangesAsync();

            var savedStudent = student;
            savedStudent.StudentId = dbStudent.StudentId;
            return savedStudent;
        }

        public async Task<IEnumerable<Logic.Model.Student>> GetStudents()
        {
            var result = await _applicationDbContext.Students.Select(student => new Logic.Model.Student()
            {
                Name = student.Name,
                Email = student.Email,
                StudentId = student.StudentId
            }).ToListAsync();

            return result;
        }

        public async Task DeleteStudent(int studentId)
        {
            var student = _applicationDbContext.Students.Find(studentId);
            _applicationDbContext.Remove(student);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
