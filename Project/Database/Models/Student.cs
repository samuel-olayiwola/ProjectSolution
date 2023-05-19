using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Project.Database.Models
{
    public class Student
    {
        [Key]
        [IgnoreDataMember]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Class { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

}


