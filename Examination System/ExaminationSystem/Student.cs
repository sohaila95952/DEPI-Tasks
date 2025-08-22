using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem
{
    public class Student
    {
        public int Id { get;private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public List<Course> EnrolledCourses { get; private set; } = new List<Course>();
        public Dictionary<Exam, int> ExamScores { get; private set; }
        public Student(int id,string name,string email)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            ExamScores = new Dictionary<Exam, int>();
        }
        public void EnrollInCourse(Course course) { 
        if(course != null&& !EnrolledCourses.Contains(course))
            {
                EnrolledCourses.Add(course);
            }
        }

    }
}
