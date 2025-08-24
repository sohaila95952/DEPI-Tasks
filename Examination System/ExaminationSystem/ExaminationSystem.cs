using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem
{
    public class ExaminationSystem
    {
        public List<Exam> Exams {  get;private set; }
        public List<Course> Courses { get; private set; }
        public List<Instructor> Instructors { get; private set; }
        public List<Student> Students {  get; private set; }
        public List<StudentExam> studentExams { get; private set; }
        public ExaminationSystem()
        {
            Exams = new List<Exam>();
            Courses = new List<Course>();
            Instructors = new List<Instructor>();
            Students = new List<Student>();
            studentExams = new List<StudentExam>();
        }
        public void AssignTextScore(Student student,Exam exam,TextQuestion textQuestion,int score)
        {
            StudentExam studentExam = null;
            foreach (var student_exam in studentExams)
            {
                if (student_exam.Student == student && student_exam.Exam == exam)
                {
                    studentExam = student_exam;
                    break;
                }
            }
            if (studentExam == null)
                throw new InvalidOperationException($"No StudentExam found for student {student.Name} in exam {exam.Title}.");
            if (!exam.Questions.Contains(textQuestion))
            {
                throw new InvalidOperationException("This Exam doesnt Contain Text Questions");
            }
            if (score<0||score>textQuestion.Marks)
            {
                throw new ArgumentOutOfRangeException($"Score Must be between 0 and {textQuestion.Marks}");
            }
            studentExam.ManualTextQuestionMarks[textQuestion]=score;
            studentExam.CalculateScore();
            Console.WriteLine($"Score of {score} assigned to Text question '{textQuestion.Text}' for {student.Name} in {exam.Course.Title} exam.");
            Console.WriteLine($"Updated total score for {student.Name} in {exam.Title}: {student.ExamScores[exam]}/{exam.TotalMarks}");

        }
        public void AddCourse(Course course)
        {
            Courses.Add(course);
        }
        public void AddInstructor(Instructor instructor)
        {
            Instructors.Add(instructor);
        }
        public void AddStudent(Student student) 
        { 
            Students.Add(student);
        }
        public void AddExam(Exam exam)
        {
            Exams.Add(exam);
        }
        public void AddStudentExam(StudentExam studentExam)
        {
            studentExams.Add(studentExam);
        }
        public void GenerateExamReport(Exam exam)
        {
            if (exam == null)
            {
                Console.WriteLine("Error: Exam cannot be null for report generation.");
                return;
            }
            Console.WriteLine($"--- Exam Report for '{exam.Title}' ({exam.Course.Title}) ---");
            Console.WriteLine($"Total Marks: {exam.TotalMarks}");
            Console.WriteLine("--------------------------------------------------");
            List<Student> studentsWhoTookExam = new List<Student>();
            foreach (var studentExam in studentExams)
            {
                if (studentExam.Exam == exam && !studentsWhoTookExam.Contains(studentExam.Student))
                {
                    studentsWhoTookExam.Add(studentExam.Student);
                }
            }

            if (studentsWhoTookExam.Count == 0)
            {
                Console.WriteLine("No students have taken this exam yet.");
                return;
            }

            foreach (var student in studentsWhoTookExam)
            {
                int score = student.ExamScores.ContainsKey(exam) ? student.ExamScores[exam] : 0;
              
                
                Console.WriteLine($"Student: {student.Name}, Score: {score}/{exam.TotalMarks}");
            }
            Console.WriteLine("--------------------------------------------------");
        }
        public void CompareStudents(Student student1, Student student2, Exam exam)
        {
            if (!Students.Contains(student1) || !Students.Contains(student2))
                throw new InvalidOperationException("One or both students not found in the system");
            if (!Exams.Contains(exam))
                throw new InvalidOperationException("Exam not found in the system");

            int score1 = student1.ExamScores[exam]; 
            int score2 = student2.ExamScores[exam];

            Console.WriteLine($"--- Comparison for {exam.Title} ---");
            Console.WriteLine($"{student1.Name}: {score1}/{exam.TotalMarks}");
            Console.WriteLine($"{student2.Name}: {score2}/{exam.TotalMarks}");
            if (score1 > score2)
                Console.WriteLine($"{student1.Name} scored higher.");
            else if (score2 > score1)
                Console.WriteLine($"{student2.Name} scored higher.");
            else
                Console.WriteLine("Both students scored the same.");
        }
        public void EnrollStudentInCourse(Student student, Course course)
        {
            if (!Students.Contains(student))
                throw new InvalidOperationException("Student not found in the system");
            if (!Courses.Contains(course))
                throw new InvalidOperationException("Course not found in the system");

            student.EnrollInCourse(course);
            Console.WriteLine($"{student.Name} enrolled in {course.Title}.");
        }
        public void AssignInstructorToCourse(Instructor instructor, Course course)
        {
            if (!Instructors.Contains(instructor))
                throw new InvalidOperationException("Instructor not found in the system");
            if (!Courses.Contains(course))
                throw new InvalidOperationException("Course not found in the system");

            instructor.TeachCourse(course);
            Console.WriteLine($"{instructor.Name} assigned to teach {course.Title}.");
        }
        public void TakeExam(Student student, Exam exam)
        {
            if (!Students.Contains(student))
                throw new InvalidOperationException("Student not found in the system");
            if (!Exams.Contains(exam))
                throw new InvalidOperationException("Exam not found in the system");
            if (!student.EnrolledCourses.Contains(exam.Course))
                throw new InvalidOperationException("Student is not enrolled in the course");

            var studentExam = new StudentExam(student, exam);
            AddStudentExam(studentExam);
            if (!studentExams.Contains(studentExam)) 
            {
                studentExams.Add(studentExam);
                Console.WriteLine($"Added StudentExam for {student.Name} in {exam.Title} to StudentExams.");
            }
            studentExam.TakeExam();
        }
    }
}
