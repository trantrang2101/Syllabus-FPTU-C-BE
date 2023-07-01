using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Account : BasicModel
    {
        public Account()
        {
            AccountRoles = new HashSet<AccountRole>();
            Courses = new HashSet<Course>();
            StudentCourses = new HashSet<StudentCourse>();
            StudentProgresses = new HashSet<StudentProgress>();
        }

        public string? Code { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public int? Role { get; set; }

        public virtual ICollection<AccountRole> AccountRoles { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
        public virtual ICollection<StudentProgress> StudentProgresses { get; set; }
    }
}
