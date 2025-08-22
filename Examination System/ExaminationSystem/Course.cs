using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem
{
    public class Course
    {

        public string Title { get; private set; }
        public string Description { get; private set; }
        public int MaxDegree {  get; private set; }

        public Course(string title,string description,int max_degree)
        {
            if(string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Course title cannot be empty.");

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Course description cannot be empty.");

            if (max_degree <= 0)
                throw new ArgumentException("Max degree must be greater than 0.");
            Title = title;
            Description = description;
            MaxDegree = max_degree;
        }

    }
}
