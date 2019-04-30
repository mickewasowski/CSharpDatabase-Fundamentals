using System;
using System.Collections.Generic;
using System.Text;

namespace P01_StudentSystem.Data.Models
{
    public class Student
    {
        public Student()
        {
        }

        public Student(
            string name,
            string phoneNumber = null,
            DateTime? birthday = null
            )
        {
            this.Name = name;
            this.PhoneNumber = phoneNumber;
            this.Birthday = birthday;


            this.CourseEnrollments = new List<StudentCourse>();
            this.HomeworkSubmissions = new List<Homework>();
        }

        public int StudentId { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime? Birthday { get; set; } //nullable datetime

        public ICollection<StudentCourse> CourseEnrollments { get; set; }

        public ICollection<Homework> HomeworkSubmissions { get; set; }

    }
}
