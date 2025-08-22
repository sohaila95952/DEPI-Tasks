using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem
{
    public class Exam
    {
        public string Title {  get;private set; }
        public Course Course {  get;private set; }
        public List<Question> Questions { get;private set; }
        public bool isStarted {  get;private set; }
        public int TotalMarks { 
            get {
                int total_marks = 0;
                
                foreach (var item in Questions)
                    total_marks += item.Marks;
                return total_marks;
            }
            }
        public Exam(Course course, string title)
        {
            Title = title;
            Course = course;
            Questions = new List<Question>();
            isStarted = false;
            
        }
        public void AddQuestion(Question question)
        {
            if (isStarted)
                throw new InvalidOperationException("Modifying an exam once it starts is Prevented");
            int currentTotal = 0;
            foreach (Question question1 in Questions)
            {
                currentTotal += question1.Marks;
            }
            if (currentTotal + question.Marks > Course.MaxDegree)
            {
                throw new InvalidOperationException("Cant Add...Total Marks would Exceed the Course Maximum Degree");
            }
           
            Questions.Add(question);
        }
        public void RemoveQuestion(Question question) {
            if (isStarted)
                throw new InvalidOperationException("Cant Remove...Exam has Started");
            Questions.Remove(question);
        }
        public void EditQuestion(Question oldquestion,Question newquestion)
        {
            if (isStarted)
                throw new InvalidOperationException("Cant Edit...Exam has Started");
            int oldindex=Questions.IndexOf(oldquestion);
            //int oldTotal = 0;
            if (oldindex != -1)
            {
                int newTotal = TotalMarks - oldquestion.Marks + newquestion.Marks;
                if (newTotal > Course.MaxDegree)
                { throw new InvalidOperationException("Cant Edit...Total Marks would Exceed the Course Maximum Degree"); }
                Questions[oldindex] = newquestion;

            }

        }
       public void StartExam()
        {
            isStarted=true;
        }
        public Exam DuplicateExam(Course newcourse,string title)
        {
            Exam DuplicatedExam=new Exam(newcourse,title);
            foreach(Question question in Questions)
            { DuplicatedExam.Questions.Add(question);}
            return DuplicatedExam;

        }
    }
}
