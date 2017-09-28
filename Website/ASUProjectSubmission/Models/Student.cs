using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASUProjectSubmission.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid StudentId { get; set; }
        public int ASUStudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string APIUrl { get; set; }
        public string APIKey { get; set; }
        public string RequestBody { get; set; }
        public string APIResponse { get; set; }
        public bool APIIsValid { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
