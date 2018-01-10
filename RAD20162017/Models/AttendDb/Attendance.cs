using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RAD20162017.Models.AttendDb
{
    [Table("Attendance")]
    public class Attendance
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttendanceID { get; set; }

        public DateTime AttendanceDate { get; set; }

        [ForeignKey("subject")]
        public int SubjectID { get; set; }

        [ForeignKey("student")]
        public int StudentID { get; set; }

        public bool Present { get; set; }


        public virtual Student student { get; set; }
        public virtual Subject subject { get; set; }


    }
}