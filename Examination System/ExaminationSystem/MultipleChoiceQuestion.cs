using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem
{
    public class MultipleChoiceQuestion:Question
    {
        public List<string> Options {  get;private set; }
        public int CorrectOption { get; set; }
        public MultipleChoiceQuestion(string text,int marks, List<string> options,int correctoption) :base(text,marks)
        {
            if (options == null || options.Count < 2)
                throw new ArgumentException("A multiple choice question must have at least two options.");
            if (CorrectOption < 0 || correctoption >= options.Count)
                throw new ArgumentOutOfRangeException(nameof(correctoption), "Correct option index is out of range.");
            Options =options;
            CorrectOption=correctoption;
        }
        public override void DisplayQuestion()
        {
            Console.WriteLine($"Question {Id} : {Text} . ({Marks} Marks)");
            for (int i = 0; i < Options.Count; i++) 
            {

                    Console.WriteLine($"{i+1} . {Options[i]} .");

            }
        }
        public override bool CheckAnswer(string studentAnswer)
        {
            return int.Parse(studentAnswer) - 1 == CorrectOption;
        }

            //if (string.IsNullOrEmpty(studentAnswer) ) 
            //{ 
            //    return false; 
            //}
            //int ChosenOptionIndex;
            //if (!int.TryParse(studentAnswer,out ChosenOptionIndex))
            //{
            //    return false;
            //}
            //ChosenOptionIndex--;
            //if (ChosenOptionIndex < 0 || ChosenOptionIndex >= Options.Count)
            //{
            //    return false;
            //}

            //if (ChosenOptionIndex == CorrectOption)
            //{
            //    return true;
            //}

            //return ChosenOptionIndex ==CorrectOption;

        }
    }

