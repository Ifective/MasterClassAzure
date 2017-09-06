using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data
{
    public class Enrolment
    {
        [Key]
        public int EnrolmentId { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
