namespace ExaminationSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ExaminationSystem system = new ExaminationSystem();
            Course course = new Course("Math1", "Introduction to Mathematics", 100);
            system.AddCourse(course);
            Student student1 = new Student(1, "Sohaila", "Sohaila@gmail.com");
            Student student2 = new Student(2, "Salma", "Salmabasma@gmail.com");
            system.AddStudent(student1);
            system.AddStudent(student2);
            Instructor instructor = new Instructor(1, "Dr. Ahmed", "Mathematics");
            system.AddInstructor(instructor);
            system.EnrollStudentInCourse(student1, course);
            system.EnrollStudentInCourse(student2, course);
            system.AssignInstructorToCourse(instructor, course);
            var exam = new Exam(course, "Midterm Exam");
            var mcq = new MultipleChoiceQuestion("What is 2+2?", 20, new List<string> { "2","3", "4", "5" }, 2);
            var tfq = new TrueFalseQuestion("Is the sky blue?", 10, true);
            var textq = new TextQuestion("Explain calculus.", 30);
            exam.AddQuestion(mcq);
            exam.AddQuestion(tfq);
            exam.AddQuestion(textq);
            system.AddExam(exam);
            var course2 = new Course("Math2", "Advanced Mathematics", 100);
            system.AddCourse(course2);
            var duplicatedExam = exam.DuplicateExam(course2, "Final Exam");
            system.AddExam(duplicatedExam);
            Console.WriteLine("=== Sohaila takes the exam ===");
            system.TakeExam(student1, exam);
            Console.WriteLine("=== Salma takes the exam ===");
            system.TakeExam(student2, exam);
            system.AssignTextScore(student1, exam, textq, 25);
            system.AssignTextScore(student2, exam, textq, 20);
            system.GenerateExamReport(exam);
            system.CompareStudents(student1, student2, exam);
        }
    
    }
}
