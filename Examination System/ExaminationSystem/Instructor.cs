using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem
{
    public class Instructor
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Specialization { get; private set; }
        public List<Course> TaughtCourses { get; private set; } = new List<Course>();
        public Instructor(int id,string name,string specialization)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Instructor name cannot be empty.");
            if (string.IsNullOrWhiteSpace(specialization))
                throw new ArgumentException("Instructor specialization cannot be empty.");
            this.Id = id;
            this.Name = name;
            this.Specialization = specialization;
        }
        public void TeachCourse(Course course)
        {
            if (course != null && !TaughtCourses.Contains(course))
            {
                TaughtCourses.Add(course);
            }
        }
    }
}
