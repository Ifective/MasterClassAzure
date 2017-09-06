using Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentRegistration.WebApp.Models
{
    public class SearchViewModel
    {
        public string Query { get; set; }
        public IEnumerable<SearchStudent> Students { get; set; }
        public SearchViewModel()
        {
            Students = new List<SearchStudent>();
        }
    }
}
