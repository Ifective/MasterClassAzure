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
    public class SearchLogic : ISearchLogic
    {
        private readonly SearchSettings _searchSettings;

        public SearchLogic(IOptions<SearchSettings> searchSettings)
        {
            _searchSettings = searchSettings.Value;
        }

        public async Task AddStudentToSearchIndex(Student student)
        {
            var indexClient = CreateIndexClient();
            var batch = IndexBatch.Upload(new[] { new SearchStudent()
            {
                StudentId = Convert.ToString(student.StudentId),
                Name = student.Name,
                Email = student.Email
            } });
            await indexClient.Documents.IndexAsync(batch);
        }

        public async Task<IEnumerable<SearchStudent>> SearchStudents(string query)
        {
            var indexClient = CreateIndexClient();
            var searchResult = await indexClient.Documents.SearchAsync<SearchStudent>(query);            
            return searchResult.Results.Select(r => r.Document);
        }

        private ISearchIndexClient CreateIndexClient()
        {
            var serviceClient =
                new SearchServiceClient(_searchSettings.SearchService, new SearchCredentials(_searchSettings.ApiKey));
            var indexClient = serviceClient.Indexes.GetClient(_searchSettings.IndexName);
            return indexClient;
        }
    }
}
