using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Project.Database.Models
{
    public class Grade
    {
        [Key]
        [IgnoreDataMember]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}

