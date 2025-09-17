using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ef2
{
    public class ExaminationDbContext : DbContext
    {

        public ExaminationDbContext(DbContextOptions<ExaminationDbContext> options)
            : base(options) { }
        public ExaminationDbContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ExaminationDB;Trusted_Connection=True;");
            }
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }
        public DbSet<TrueFalseQuestion> TrueFalseQuestions { get; set; }
        public DbSet<EssayQuestion> EssayQuestions { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<InstructorCourse> InstructorCourses { get; set; }
        public DbSet<ExamAttempt> ExamAttempts { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureEntities(modelBuilder);
            SeedInitialData(modelBuilder);
        }
        private void ConfigureEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(b =>
            {
                b.HasKey(c => c.ID);
                b.Property(c => c.Title).IsRequired().HasMaxLength(200);
                b.Property(c => c.Description).HasMaxLength(1000);
                b.Property(c => c.MaximumDegree).IsRequired().HasColumnType("decimal(18,2)");
                b.Property(c => c.CreatedDate).IsRequired();
                b.Property(c => c.IsActive).HasDefaultValue(true);
                b.HasCheckConstraint("CK_Course_MaximumDegree_Positive", "MaximumDegree > 0");
                b.HasMany(c => c.Exams).WithOne(e => e.Course)
                 .HasForeignKey(e => e.CourseID)
                 .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Student>(b =>
            {
                b.HasKey(s => s.ID);
                b.Property(s => s.Name).IsRequired().HasMaxLength(100);
                b.Property(s => s.Email).IsRequired();
                b.Property(s => s.StudentNumber).IsRequired().HasMaxLength(20);
                b.Property(s => s.EnrollmentDate).IsRequired();
                b.Property(s => s.IsActive).HasDefaultValue(true);
                b.HasIndex(s => s.Email).IsUnique();
                b.HasIndex(s => s.StudentNumber).IsUnique();
                b.HasMany(s => s.ExamAttempts).WithOne(s => s.Student)
                 .HasForeignKey(s => s.StudentID)
                 .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Instructor>(b =>
            {
                b.HasKey(i => i.ID);
                b.Property(i => i.Name).IsRequired().HasMaxLength(100);
                b.Property(i => i.Email).IsRequired();
                b.Property(i => i.Specialization).IsRequired().HasMaxLength(150);
                b.Property(i => i.HireDate).IsRequired();
                b.Property(i => i.IsActive).HasDefaultValue(true);
                b.HasIndex(i => i.Email).IsUnique();
                b.HasMany(i => i.Exams).WithOne(s => s.Instructor)
                 .HasForeignKey(s => s.InstructorId)
                 .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Exam>(b =>
            {
                b.HasKey(e => e.ID);
                b.Property(e => e.Title).IsRequired().HasMaxLength(200);
                b.Property(e => e.Description).HasMaxLength(500);
                b.Property(e => e.TotalMarks).IsRequired().HasColumnType("decimal(18,2)");
                b.Property(e => e.Duration).IsRequired();
                b.Property(e => e.StartDate).IsRequired();
                b.Property(e => e.EndDate).IsRequired();
                b.Property(e => e.IsActive).HasDefaultValue(true);
                b.HasIndex(e => e.StartDate);
                b.HasCheckConstraint("CK_Exam_EndDateAfterStart", "EndDate > StartDate");
                b.HasMany(e => e.Questions).WithOne(s => s.Exam)
                 .HasForeignKey(s => s.ExamID)
                 .OnDelete(DeleteBehavior.Cascade);
                b.HasMany(e => e.ExamAttempts).WithOne(s => s.Exam)
                 .HasForeignKey(s => s.ExamID)
                 .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Question>(b =>
            {
                b.HasKey(q => q.ID);
                b.Property(q => q.QuestionText).IsRequired().HasMaxLength(1000);
                b.Property(q => q.Marks).IsRequired().HasColumnType("decimal(18,2)");
                b.Property(q => q.CreatedDate).IsRequired();
                b.HasCheckConstraint("CK_Question_Marks_Positive", "Marks > 0");
                b.HasDiscriminator<QuestionType>(q => q.QuestionType)
                    .HasValue<MultipleChoiceQuestion>(QuestionType.MultipleChoice)
                    .HasValue<TrueFalseQuestion>(QuestionType.TrueFalse)
                    .HasValue<EssayQuestion>(QuestionType.Essay);
            });
            modelBuilder.Entity<MultipleChoiceQuestion>(b =>
            {
                b.Property(m => m.OptionA).IsRequired().HasMaxLength(500);
                b.Property(m => m.OptionB).IsRequired().HasMaxLength(500);
                b.Property(m => m.OptionC).IsRequired().HasMaxLength(500);
                b.Property(m => m.OptionD).IsRequired().HasMaxLength(500);
                b.Property(m => m.CorrectOption).IsRequired();
            });
            modelBuilder.Entity<TrueFalseQuestion>(b =>
            {
                b.Property(tf => tf.CorrectAnswer).IsRequired();
            });
            modelBuilder.Entity<EssayQuestion>(b =>
            {
                b.Property(e => e.MaxWordCount).IsRequired(false);
                b.Property(e => e.GradingCriteria).HasMaxLength(1000);
            });
            modelBuilder.Entity<StudentCourse>(b =>
            {
                b.HasKey(sc => new { sc.StudentID, sc.CourseID });
                b.Property(sc => sc.EnrollmentDate).IsRequired();
                b.Property(sc => sc.Grade).IsRequired(false).HasColumnType("decimal(18,2)");
                b.Property(sc => sc.IsCompleted).HasDefaultValue(false);
                b.HasOne(sc => sc.Student).WithMany(sc => sc.StudentCourses)
                 .HasForeignKey(sc => sc.StudentID)
                 .OnDelete(DeleteBehavior.Cascade);
                b.HasOne(sc => sc.Course).WithMany(sc => sc.StudentCourses)
                 .HasForeignKey(sc => sc.CourseID)
                 .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<InstructorCourse>(b =>
            {
                b.HasKey(ic => new { ic.InstructorID, ic.CourseID });
                b.Property(ic => ic.AssignedDate).IsRequired();
                b.Property(ic => ic.IsActive).HasDefaultValue(true);
                b.HasOne(ic => ic.Instructor).WithMany(ic => ic.InstructorCourses)
                 .HasForeignKey(ic => ic.InstructorID)
                 .OnDelete(DeleteBehavior.Cascade);
                b.HasOne(ic => ic.Course).WithMany(ic => ic.InstructorCourses)
                 .HasForeignKey(ic => ic.CourseID)
                 .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<ExamAttempt>(b =>
            {
                b.HasKey(ea => ea.ID);
                b.Property(ea => ea.StartTime).IsRequired();
                b.Property(ea => ea.EndTime).IsRequired(false);
                b.Property(ea => ea.TotalScore).IsRequired(false).HasColumnType("decimal(18,2)");
                b.Property(ea => ea.IsSubmitted).HasDefaultValue(false);
                b.Property(ea => ea.IsGraded).HasDefaultValue(false);
                b.HasIndex(ea => ea.StartTime);
                b.HasMany(ea => ea.StudentAnswers).WithOne(ea => ea.ExamAttempt)
                 .HasForeignKey(ea => ea.ExamAttemptID)
                 .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<StudentAnswer>(b =>
            {
                b.HasKey(sa => sa.ID);
                b.Property(sa => sa.AnswerText).IsRequired().HasMaxLength(2000);
                b.Property(sa => sa.SelectedOption).IsRequired(false);
                b.Property(sa => sa.BooleanAnswer).IsRequired(false);
                b.Property(sa => sa.MarksObtained).IsRequired(false).HasColumnType("decimal(18,2)");
                b.Property(sa => sa.SubmittedAt).IsRequired();
                b.HasOne(sa => sa.ExamAttempt).WithMany(sa => sa.StudentAnswers)
                 .HasForeignKey(sa => sa.ExamAttemptID)
                 .OnDelete(DeleteBehavior.Cascade);
                b.HasOne(sa => sa.Question).WithMany(sa => sa.StudentAnswers)
                 .HasForeignKey(sa => sa.QuestionId)
                 .OnDelete(DeleteBehavior.Restrict);
            });
        }
        private void SeedInitialData(ModelBuilder modelBuilder)
        {
            var c1 = new Course
            {
                ID = 1,
                Title = "Object-Oriented Programming",
                Description = "Principles of OOP and C#",
                MaximumDegree = 100m,
                CreatedDate = new DateTime(2024, 2, 1),
                IsActive = true
            };
            var c2 = new Course
            {
                ID = 2,
                Title = "Computer Networks",
                Description = "Fundamentals of networking and protocols",
                MaximumDegree = 100m,
                CreatedDate = new DateTime(2024, 3, 1),
                IsActive = true
            };
            var c3 = new Course
            {
                ID = 3,
                Title = "Operating Systems",
                Description = "Processes, memory, and file management",
                MaximumDegree = 100m,
                CreatedDate = new DateTime(2024, 4, 1),
                IsActive = true
            };
            modelBuilder.Entity<Course>().HasData(c1, c2, c3);

            var s1 = new Student
            {
                ID = 1,
                Name = "Sohaila Ahmed",
                Email = "Sohaila@gmail.com",
                StudentNumber = "s2001",
                EnrollmentDate = new DateTime(2024, 9, 5),
                IsActive = true
            };
            var s2 = new Student
            {
                ID = 2,
                Name = "Khaled Youssef",
                Email = "khaled@gmail.com",
                StudentNumber = "s2002",
                EnrollmentDate = new DateTime(2024, 9, 10),
                IsActive = true
            };
            var s3 = new Student
            {
                ID = 3,
                Name = "Mariam Adel",
                Email = "mariam@gmail.com",
                StudentNumber = "s2003",
                EnrollmentDate = new DateTime(2024, 9, 12),
                IsActive = true
            };
            var s4 = new Student
            {
                ID = 4,
                Name = "Ahmed Samir",
                Email = "ahmed@gmail.com",
                StudentNumber = "s2004",
                EnrollmentDate = new DateTime(2024, 9, 15),
                IsActive = true
            };
            var s5 = new Student
            {
                ID = 5,
                Name = "Nour Hassan",
                Email = "nour@gmail.com",
                StudentNumber = "s2005",
                EnrollmentDate = new DateTime(2024, 9, 20),
                IsActive = true
            };
            modelBuilder.Entity<Student>().HasData(s1, s2, s3, s4, s5);

            var i1 = new Instructor
            {
                ID = 1,
                Name = "Dr. Yasser Ibrahim",
                Email = "yasser.ibrahim@gmail.com",
                Specialization = "Computer Science",
                HireDate = new DateTime(2019, 1, 15),
                IsActive = true
            };
            var i2 = new Instructor
            {
                ID = 2,
                Name = "Prof. Mona Fathy",
                Email = "mona.fathy@gmail.com",
                Specialization = "Networks and Security",
                HireDate = new DateTime(2021, 3, 1),
                IsActive = true
            };
            modelBuilder.Entity<Instructor>().HasData(i1, i2);

            var ex1 = new Exam
            {
                ID = 1,
                Title = "OOP Midterm",
                Description = "C# classes and inheritance",
                TotalMarks = 50m,
                Duration = TimeSpan.FromHours(2),
                StartDate = new DateTime(2024, 10, 10, 9, 0, 0),
                EndDate = new DateTime(2024, 10, 10, 11, 0, 0),
                CourseID = 1,
                InstructorId = 1,
                IsActive = true
            };
            var ex2 = new Exam
            {
                ID = 2,
                Title = "Networks Final",
                Description = "Covers OSI model, TCP/IP, routing",
                TotalMarks = 100m,
                Duration = TimeSpan.FromHours(3),
                StartDate = new DateTime(2024, 12, 20, 10, 0, 0),
                EndDate = new DateTime(2024, 12, 20, 13, 0, 0),
                CourseID = 2,
                InstructorId = 2,
                IsActive = true
            };
            modelBuilder.Entity<Exam>().HasData(ex1, ex2);

            modelBuilder.Entity<MultipleChoiceQuestion>().HasData(new
            {
                ID = 1,
                QuestionText = "Which OOP concept allows creating multiple methods with the same name but different parameters?",
                Marks = 5m,
                QuestionType = QuestionType.MultipleChoice,
                CreatedDate = new DateTime(2024, 9, 25),
                ExamID = 1,
                OptionA = "Inheritance",
                OptionB = "Polymorphism",
                OptionC = "Encapsulation",
                OptionD = "Abstraction",
                CorrectOption = 'B'
            });
            modelBuilder.Entity<TrueFalseQuestion>().HasData(new
            {
                ID = 2,
                QuestionText = "The OSI model has 7 layers.",
                Marks = 5m,
                QuestionType = QuestionType.TrueFalse,
                CreatedDate = new DateTime(2024, 11, 1),
                ExamID = 2,
                CorrectAnswer = true
            });
            modelBuilder.Entity<EssayQuestion>().HasData(new
            {
                ID = 3,
                QuestionText = "Discuss the differences between TCP and UDP with examples.",
                Marks = 10m,
                QuestionType = QuestionType.Essay,
                CreatedDate = new DateTime(2024, 11, 2),
                ExamID = 2,
                MaxWordCount = 400,
                GradingCriteria = "Accuracy, technical depth, clarity"
            });

            modelBuilder.Entity<StudentCourse>().HasData(
                new { StudentID = 1, CourseID = 1, EnrollmentDate = new DateTime(2024, 9, 5), Grade = (decimal?)null, IsCompleted = false },
                new { StudentID = 2, CourseID = 1, EnrollmentDate = new DateTime(2024, 9, 10), Grade = (decimal?)null, IsCompleted = false },
                new { StudentID = 3, CourseID = 2, EnrollmentDate = new DateTime(2024, 9, 12), Grade = (decimal?)null, IsCompleted = false }
            );

            modelBuilder.Entity<InstructorCourse>().HasData(
                new { InstructorID = 1, CourseID = 1, AssignedDate = new DateTime(2024, 2, 10), IsActive = true },
                new { InstructorID = 2, CourseID = 2, AssignedDate = new DateTime(2024, 3, 15), IsActive = true }
            );
        }


    }
}