using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Project.Database.Models
{
    public class StudentCourse
    {
        [Key]
        [IgnoreDataMember]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int GradeId { get; set; }

    }
}

