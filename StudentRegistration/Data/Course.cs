using System.ComponentModel.DataAnnotations;

namespace Data
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string Name { get; set; }
    }
}
