using System.Collections.Generic;
using Logic.Model;
using System.Threading.Tasks;
using System;

namespace Logic.Services
{
    public interface IStudentService
    {
        Task<Student> SaveStudent(Student student);
        Task<IEnumerable<Student>> GetStudents();
        Task DeleteStudent(int studentId);
    }
}