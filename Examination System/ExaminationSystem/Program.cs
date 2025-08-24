using System;
using System.Collections.Generic;
using System.Linq;

namespace ExaminationSystem
{
    class Program
    {
        static ExaminationSystem system = new ExaminationSystem();
        static Student Student = null;
        static Instructor Instructor = null;

        static void Main(string[] args)
        {
            var mathCourse = new Course("Math1", "Mathematics Course", 100);
            system.AddCourse(mathCourse);
            var instructor = new Instructor(1, "Ahmed", "Mathematics");
            system.AddInstructor(instructor);
            var student1 = new Student(1, "Sohaila", "sohaila@gmail.com");
            var student2 = new Student(2, "Salma", "salma@gmail.com");
            system.AddStudent(student1);
            system.AddStudent(student2);

            while (true)
            {
                DisplayMainMenu();
            }
        }

        static void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Examination System ===");
            Console.WriteLine("1. Login as Instructor");
            Console.WriteLine("2. Login as Student");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice (1-3): ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    InstructorLogin();
                    break;
                case "2":
                    StudentLogin();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Press any key to try again...");
                    Console.ReadKey();
                    break;
            }
        }

        static void InstructorLogin()
        {
            Console.Clear();
            Console.WriteLine("=== Instructor Login ===");
            Console.Write("Enter Instructor Name: ");
            string name = Console.ReadLine();
            Instructor = null;
            foreach (var instructor in system.Instructors)
            {
                if (instructor.Name == name)
                {
                    Instructor = instructor;
                    break;
                }
            }
            if (Instructor == null)
            {
                Console.WriteLine("Instructor not found. Press any key to return...");
                Console.ReadKey();
                return;
            }
            InstructorMenu();
        }

        static void StudentLogin()
        {
            Console.Clear();
            Console.WriteLine("=== Student Login ===");
            Console.Write("Enter Student Name: ");
            string name = Console.ReadLine();
            Student = null;
            foreach (var student in system.Students)
            {
                if (student.Name == name)
                {
                    Student = student;
                    break;
                }
            }
            if (Student == null)
            {
                Console.WriteLine("Student not found. Press any key to return...");
                Console.ReadKey();
                return;
            }
            StudentMenu();
        }

        static void InstructorMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== Instructor Menu ({Instructor.Name}) ===");
                Console.WriteLine("1. Add Course");
                Console.WriteLine("2. Assign Instructor to Course");
                Console.WriteLine("3. Create Exam");
                Console.WriteLine("4. Assign Text Question Score");
                Console.WriteLine("5. Generate Exam Report");
                Console.WriteLine("6. Compare Students");
                Console.WriteLine("7. Back");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddCourse();
                        break;
                    case "2":
                        AssignInstructorToCourse();
                        break;
                    case "3":
                        CreateExam();
                        break;
                    case "4":
                        AssignTextScore();
                        break;
                    case "5":
                        GenerateExamReport();
                        break;
                    case "6":
                        CompareStudents();
                        break;
                    case "7":
                        Instructor = null;
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void StudentMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== Student Menu ({Student.Name}) ===");
                Console.WriteLine("1. Enroll in Course");
                Console.WriteLine("2. Take Exam");
                Console.WriteLine("3. View Scores");
                Console.WriteLine("4. Back");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        EnrollInCourse();
                        break;
                    case "2":
                        TakeExam();
                        break;
                    case "3":
                        ViewScores();
                        break;
                    case "4":
                        Student = null;
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void AddCourse()
        {
            Console.Clear();
            Console.WriteLine("=== Add Course ===");
            Console.Write("Enter Course Title: ");
            string title = Console.ReadLine();
            Console.Write("Enter Course Description: ");
            string description = Console.ReadLine();
            Console.Write("Enter Max Degree: ");
            if (!int.TryParse(Console.ReadLine(), out int maxDegree) || maxDegree <= 0)
            {
                Console.WriteLine("Invalid Max Degree. Press any key to return...");
                Console.ReadKey();
                return;
            }

            try
            {
                var course = new Course(title, description, maxDegree);
                system.AddCourse(course);
                Console.WriteLine($"Course {title} added successfully. Press any key to continue...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Press any key to continue...");
            }
            Console.ReadKey();
        }

        static void AssignInstructorToCourse()
        {
            Console.Clear();
            Console.WriteLine("=== Assign Instructor to Course ===");
            Console.WriteLine("Available Courses:");
            foreach (var course1 in system.Courses)
            {
                Console.WriteLine($"- {course1.Title}");
            }
            Console.Write("Enter Course Title: ");
            string courseTitle = Console.ReadLine();
            Course course = null;
            foreach (var c in system.Courses)
            {
                if (c.Title == courseTitle)
                {
                    course = c;
                    break;
                }
            }
            if (course == null)
            {
                Console.WriteLine("Course not found. Press any key to return...");
                Console.ReadKey();
                return;
            }

            try
            {
                system.AssignInstructorToCourse(Instructor, course);
                Console.WriteLine($"Instructor {Instructor.Name} assigned to {course.Title}. Press any key to continue...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Press any key to continue...");
            }
            Console.ReadKey();
        }

        static void CreateExam()
        {
            Console.Clear();
            Console.WriteLine("=== Create Exam ===");
            Console.WriteLine("Available Courses:");
            foreach (var course1 in system.Courses)
            {
                Console.WriteLine($"- {course1.Title}");
            }
            Console.Write("Enter Course Title: ");
            string courseTitle = Console.ReadLine();
            Course course = null;
            foreach (var c in system.Courses)
            {
                if (c.Title == courseTitle)
                {
                    course = c;
                    break;
                }
            }
            if (course == null)
            {
                Console.WriteLine("Course not found. Press any key to return...");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter Exam Title: ");
            string examTitle = Console.ReadLine();
            var exam = new Exam(course, examTitle);

            while (true)
            {
                Console.WriteLine("Add Question:");
                Console.WriteLine("1. Multiple Choice");
                Console.WriteLine("2. True/False");
                Console.WriteLine("3. Text/Essay");
                Console.WriteLine("4. End Exam");
                Console.Write("Enter choice: ");
                string questionChoice = Console.ReadLine();

                if (questionChoice == "4")
                    break;

                Console.Write("Enter Question Text: ");
                string questionText = Console.ReadLine();
                Console.Write("Enter Question Marks: ");
                if (!int.TryParse(Console.ReadLine(), out int marks) || marks <= 0)
                {
                    Console.WriteLine("Invalid marks. Try again...");
                    continue;
                }

                try
                {
                    if (questionChoice == "1")
                    {
                        var options = new List<string>();
                        Console.WriteLine("Enter options (enter 'done' to finish):");
                        while (true)
                        {
                            string option = Console.ReadLine();
                            if (option.ToLower() == "done")
                                break;
                            options.Add(option);
                        }
                        Console.Write("Enter correct option index (0-based): ");
                        if (!int.TryParse(Console.ReadLine(), out int correctIndex) || correctIndex < 0 || correctIndex >= options.Count)
                        {
                            Console.WriteLine("Invalid correct index. Try again...");
                            continue;
                        }
                        var mcq = new MultipleChoiceQuestion(questionText, marks, options, correctIndex);
                        exam.AddQuestion(mcq);
                    }
                    else if (questionChoice == "2")
                    {
                        Console.Write("Enter correct answer (true/false): ");
                        string correctAnswer = Console.ReadLine().ToLower();
                        if (correctAnswer != "true" && correctAnswer != "false")
                        {
                            Console.WriteLine("Invalid answer. Try again...");
                            continue;
                        }
                        var tfq = new TrueFalseQuestion(questionText, marks, correctAnswer == "true");
                        exam.AddQuestion(tfq);
                    }
                    else if (questionChoice == "3")
                    {
                        var textQ = new TextQuestion(questionText, marks);
                        exam.AddQuestion(textQ);
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Try again...");
                        continue;
                    }
                    Console.WriteLine("Question added successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}. Try again...");
                }
            }

            try
            {
                system.AddExam(exam);
                Console.WriteLine($"Exam {examTitle} created successfully. Press any key to continue...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Press any key to continue...");
            }
            Console.ReadKey();
        }

        static void AssignTextScore()
        {
            Console.Clear();
            Console.WriteLine("=== Assign Text Question Score ===");
            Console.WriteLine("Available Students:");
            foreach (var student1 in system.Students)
            {
                Console.WriteLine($"- {student1.Name}");
            }
            Console.Write("Enter Student Name: ");
            string studentName = Console.ReadLine();
            Student student = null;
            foreach (var s in system.Students)
            {
                if (s.Name == studentName)
                {
                    student = s;
                    break;
                }
            }
            if (student == null)
            {
                Console.WriteLine("Student not found. Press any key to return...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Available Exams:");
            foreach (var exam1 in system.Exams)
            {
                Console.WriteLine($"- {exam1.Title} ({exam1.Course.Title})");
            }
            Console.Write("Enter Exam Title: ");
            string examTitle = Console.ReadLine();
            Exam exam = null;
            foreach (var e in system.Exams)
            {
                if (e.Title == examTitle)
                {
                    exam = e;
                    break;
                }
            }
            if (exam == null)
            {
                Console.WriteLine("Exam not found. Press any key to return...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Text Questions:");
            var textQuestions = exam.Questions.OfType<TextQuestion>().ToList();
            if (!textQuestions.Any())
            {
                Console.WriteLine("No text questions in this exam. Press any key to return...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < textQuestions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {textQuestions[i].Text} ({textQuestions[i].Marks} Marks)");
            }
            Console.Write("Enter Question Number: ");
            if (!int.TryParse(Console.ReadLine(), out int questionIndex) || questionIndex < 1 || questionIndex > textQuestions.Count)
            {
                Console.WriteLine("Invalid question number. Press any key to return...");
                Console.ReadKey();
                return;
            }

            Console.Write($"Enter Score (0-{textQuestions[questionIndex - 1].Marks}): ");
            if (!int.TryParse(Console.ReadLine(), out int score))
            {
                Console.WriteLine("Invalid score. Press any key to return...");
                Console.ReadKey();
                return;
            }

            try
            {
                system.AssignTextScore(student, exam, textQuestions[questionIndex - 1], score);
                Console.WriteLine("Score assigned successfully. Press any key to continue...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Press any key to continue...");
            }
            Console.ReadKey();
        }

        static void GenerateExamReport()
        {
            Console.Clear();
            Console.WriteLine("=== Generate Exam Report ===");
            Console.WriteLine("Available Exams:");
            foreach (var ex in system.Exams)
            {
                Console.WriteLine($"- {ex.Title} ({ex.Course.Title})");
            }
            Console.Write("Enter Exam Title: ");
            string examTitle = Console.ReadLine();
            Exam exam = null;
            foreach (var e in system.Exams)
            {
                if (e.Title == examTitle)
                {
                    exam = e;
                    break;
                }
            }
            if (exam == null)
            {
                Console.WriteLine("Exam not found. Press any key to return...");
                Console.ReadKey();
                return;
            }

            try
            {
                system.GenerateExamReport(exam);
                Console.WriteLine("Press any key to continue...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Press any key to continue...");
            }
            Console.ReadKey();
        }

        static void CompareStudents()
        {
            Console.Clear();
            Console.WriteLine("=== Compare Students ===");
            Console.WriteLine("Available Students:");
            foreach (var student in system.Students)
            {
                Console.WriteLine($"- {student.Name}");
            }
            Console.Write("Enter First Student Name: ");
            string student1Name = Console.ReadLine();
            Student student1 = null;
            foreach (var s in system.Students)
            {
                if (s.Name == student1Name)
                {
                    student1 = s;
                    break;
                }
            }
            if (student1 == null)
            {
                Console.WriteLine("First student not found. Press any key to return...");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter Second Student Name: ");
            string student2Name = Console.ReadLine();
            Student student2 = null;
            foreach (var s in system.Students)
            {
                if (s.Name == student2Name)
                {
                    student2 = s;
                    break;
                }
            }
            if (student2 == null)
            {
                Console.WriteLine("Second student not found. Press any key to return...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Available Exams:");
            foreach (var ex1 in system.Exams)
            {
                Console.WriteLine($"- {ex1.Title} ({ex1.Course.Title})");
            }
            Console.Write("Enter Exam Title: ");
            string examTitle = Console.ReadLine();
            Exam exam = null;
            foreach (var e in system.Exams)
            {
                if (e.Title == examTitle)
                {
                    exam = e;
                    break;
                }
            }
            if (exam == null)
            {
                Console.WriteLine("Exam not found. Press any key to return...");
                Console.ReadKey();
                return;
            }

            try
            {
                system.CompareStudents(student1, student2, exam);
                Console.WriteLine("Press any key to continue...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Press any key to continue...");
            }
            Console.ReadKey();
        }

        static void EnrollInCourse()
        {
            Console.Clear();
            Console.WriteLine("=== Enroll in Course ===");
            Console.WriteLine("Available Courses:");
            foreach (var c in system.Courses)
            {
                Console.WriteLine($"- {c.Title}");
            }
            Console.Write("Enter Course Title: ");
            string courseTitle = Console.ReadLine();
            Course course = null;
            foreach (var c in system.Courses)
            {
                if (c.Title == courseTitle)
                {
                    course = c;
                    break;
                }
            }
            if (course == null)
            {
                Console.WriteLine("Course not found. Press any key to return...");
                Console.ReadKey();
                return;
            }

            try
            {
                system.EnrollStudentInCourse(Student, course);
                Console.WriteLine($"Enrolled in {course.Title} successfully. Press any key to continue...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Press any key to continue...");
            }
            Console.ReadKey();
        }

        static void TakeExam()
        {
            Console.Clear();
            Console.WriteLine("=== Take Exam ===");
            Console.WriteLine("Available Exams:");
            foreach (var ex1 in system.Exams)
            {
                Console.WriteLine($"- {ex1.Title} ({ex1.Course.Title})");
            }
            Console.Write("Enter Exam Title: ");
            string examTitle = Console.ReadLine();
            Exam exam = null;
            foreach (var e in system.Exams)
            {
                if (e.Title == examTitle)
                {
                    exam = e;
                    break;
                }
            }
            if (exam == null)
            {
                Console.WriteLine("Exam not found. Press any key to return...");
                Console.ReadKey();
                return;
            }

            try
            {
                system.TakeExam(Student, exam);
                Console.WriteLine("Exam completed. Press any key to continue...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Press any key to continue...");
            }
            Console.ReadKey();
        }

        static void ViewScores()
        {
            Console.Clear();
            Console.WriteLine($"=== Scores for {Student.Name} ===");
            if (!Student.ExamScores.Any())
            {
                Console.WriteLine("No scores available. Press any key to return...");
                Console.ReadKey();
                return;
            }

            foreach (var score in Student.ExamScores)
            {
                Console.WriteLine($"Exam: {score.Key.Title} ({score.Key.Course.Title}), Score: {score.Value}/{score.Key.TotalMarks}");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}