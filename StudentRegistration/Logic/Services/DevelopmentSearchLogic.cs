using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Logic.Model;
using Logic.Settings;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Options;
using System.Linq;

namespace Logic.Services
{
    public class DevelopmentSearchLogic : ISearchLogic
    {        
        
        public async Task AddStudentToSearchIndex(Student student)
        {
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<SearchStudent>> SearchStudents(string query)
        {
            return await Task.FromResult(new[] { new SearchStudent() { Name = query } });
        }        
    }
}
