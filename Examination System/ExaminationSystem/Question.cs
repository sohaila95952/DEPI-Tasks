using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem
{
    public abstract class Question
    {
        private static int id = 0;
        public int Id { get; private set; }
        public string Text { get; set; }
        public int Marks { get; set; }
        public Question(string text,int marks) 
        {
            Id=id++;
            Text = text;
            Marks = marks;
        }
        public abstract void DisplayQuestion();
        public abstract bool CheckAnswer(string studentAnswer);
    }
}
