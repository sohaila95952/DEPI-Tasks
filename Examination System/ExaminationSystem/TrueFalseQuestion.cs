using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem
{
    public class TrueFalseQuestion : Question
    {
        public bool CorrectOption {  get; set; }
        public TrueFalseQuestion(string text, int marks, bool correctoption):base(text,marks)
        {
            CorrectOption = correctoption;
        }
        public override void DisplayQuestion()
        {
            Console.WriteLine($"Question {Id} : {Text} . ({Marks} Marks)");
            Console.WriteLine("Enter 'true' or 'false'");
        }
        public override bool CheckAnswer(string studentAnswer)
        {
            return bool.Parse(studentAnswer) == CorrectOption;
        }

        
    }
}
