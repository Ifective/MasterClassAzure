using Logic.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface ISearchLogic
    {
        Task AddStudentToSearchIndex(Student student);
        Task<IEnumerable<SearchStudent>> SearchStudents(string query);
    }
}