using System.ComponentModel.DataAnnotations;

namespace Data
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
