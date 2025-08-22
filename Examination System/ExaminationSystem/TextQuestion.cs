using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ExaminationSystem
{
    public class TextQuestion : Question
    {
        public TextQuestion(string text, int marks) :base(text, marks)
        {
        }
        public override void DisplayQuestion()
        {
            Console.WriteLine($"Question {Id} : {Text} . ({Marks} Marks)");
            Console.WriteLine($"Please Write Your Answer Here :)");
        }
        public override bool CheckAnswer(string studentAnswer)
        {
            return false;
        }

       
    }
}
