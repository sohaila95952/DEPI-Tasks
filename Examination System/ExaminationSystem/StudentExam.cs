using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem
{
    public class StudentExam
    {
        public Student Student { get;private set; }
        public Exam Exam { get; private set; }
        public Dictionary<Question,string> StudentAnswers { get;private set; }
        public Dictionary<TextQuestion,int> ManualTextQuestionMarks { get; private set; }
        public int Score { get; private set; }
        public bool IsCompleted { get; private set; }
        public StudentExam(Student student,Exam exam)
        {
            Student = student;
            Exam = exam;
            StudentAnswers = new Dictionary<Question, string>();
            ManualTextQuestionMarks=new Dictionary<TextQuestion, int>();
            Score = 0;
            IsCompleted = false;
        }
        public void TakeExam()
        {
            if (Exam.isStarted==false)
                Exam.StartExam();
            Console.WriteLine($"{Student.Name}..Exam is ready for you");
            foreach(var question in Exam.Questions)
            {
                question.DisplayQuestion();
                Console.WriteLine("Enter Your Answer : ");
                string answer=Console.ReadLine();
                StudentAnswers.Add(question, answer);
            }
            IsCompleted = true;
            CalculateScore();
            
            Student.ExamScores[Exam] = Score;
            //Console.WriteLine($"Your score: {Score}/{Exam.TotalMarks}");
            //Console.WriteLine($"Exam {Exam.Title} has Finished...");

        }
        public void CalculateScore()
        {
            Score = 0; 
            foreach (var question in StudentAnswers) 
            {
                Question question1 = question.Key;
                string answer = question.Value;

                if (question1 is MultipleChoiceQuestion mcq)
                {
                    if (int.TryParse(answer, out int answerIndex) && answerIndex - 1 == mcq.CorrectOption)
                    {
                        Score += mcq.Marks;
                        //Console.WriteLine($"MCQ '{mcq.Text}' scored: {mcq.Marks}");
                    }
                }
                else if (question1 is TrueFalseQuestion tfq)
                {
                    if (bool.TryParse(answer, out bool answerValue) && answerValue == tfq.CorrectOption)
                    {
                        Score += tfq.Marks;
                        //Console.WriteLine($"TFQ '{tfq.Text}' scored: {tfq.Marks}");
                    }
                }
                else if (question1 is TextQuestion textQuestion)
                {
                    if (ManualTextQuestionMarks.TryGetValue(textQuestion, out int manualScore))
                    {
                        Score += manualScore;
                        //Console.WriteLine($"Text Question '{textQuestion.Text}' scored: {manualScore}");
                    }
                }
            }
            Student.ExamScores[Exam] = Score;
            //Console.WriteLine($"Total Score for {Student.Name} in {Exam.Title}: {Score}");
            Console.WriteLine("Please wait for the instructor to grade your text questions,\n then check your results in View Scores.");
        }
    }
}
