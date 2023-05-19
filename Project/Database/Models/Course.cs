using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Project.Database.Models
{
    public class Course
    {
        [Key]
        [IgnoreDataMember]
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }
    }
}

