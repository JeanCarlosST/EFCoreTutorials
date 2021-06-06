using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreTutorials.Models
{
    public class Students
    {
        [Key]
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public float Height { get; set; }
        public float Weigth { get; set; }

        public int GradeId { get; set; }
        public Grades Grade { get; set; }

        public StudentAdresses StudentAdress { get; set; }

        public IList<StudentCourses> StudentCourses { get; set; }
    }
}
