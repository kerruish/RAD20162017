using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RAD20162017.Models.AttendDb
{
    [Table("Student")]
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentID { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public virtual ICollection<Subject> subjects { get; set; }


    }
}