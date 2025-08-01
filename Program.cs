using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Educational_Management_System;



namespace Educational_Management_System
{
    internal class Program
    {
        static List<User> Users = new List<User>(); // in-memory storage
        static List<Course> Courses = new List<Course>();
        static List<Assignment> Assignments = new List<Assignment>();
        static Assignment selectedAssignment = null;
        static Student loggedInStudent = null;
        static Doctor loggedInDoctor = null;
        static User savedUser = null;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine(" Main Menu ");
                Console.WriteLine("1. Sign Up");
                Console.WriteLine("2. Sign In");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                    SignUp();
                else if (choice == "2")
                    SignIn();
                else if (choice == "3")
                    break;
                else
                    Console.WriteLine("Invalid choice!");
            }
        }

        static void SignUp()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            Console.Write("Enter full name: ");
            string fullName = Console.ReadLine();

            Console.Write("Enter email: ");
            string email = Console.ReadLine();

            Console.Write("Are you a Doctor or Student? ");
            string role = Console.ReadLine().ToLower();

            while (true)
            {
                Console.Write("Enter email: ");
                email = Console.ReadLine();

                if (!IsValidEmail(email))
                {
                    Console.WriteLine("Invalid email format. Please try again.");
                }
                else
                {
                    break;
                }
            }
            if (role == "student")
            {
                Student student = new Student
                {
                    Username = username,
                    Password = password,
                    FullName = fullName,
                    Email = email,
                    Role = role,
                    EnrolledCourses = new List<Course>()
                };

                Users.Add(student);
                loggedInStudent = student;


                Console.WriteLine($"Welcome {student.FullName}! You are signed up as a student.");
                StudentMenu();
            }
            else if (role == "doctor")
            {
                Doctor doctor = new Doctor
                {
                    Username = username,
                    Password = password,
                    FullName = fullName,
                    Email = email,
                    Role = role,
                    MyCourses = new List<Course>()
                };

                Users.Add(doctor);
                loggedInDoctor = doctor;
                Console.WriteLine($"Welcome {doctor.FullName}! You are signed up as a doctor.");
                DoctorMenu();
            }
            else
            {
                Console.WriteLine("Invalid role. Please enter either 'Doctor' or 'Student'.");
            }
        }

        static bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        static void SignIn()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            for (int i = 0; i < Users.Count; i++)
            {
                User user = Users[i];
                if (user.Username == username && user.Password == password)
                {
                    Console.WriteLine($"Welcome back, {user.FullName}!");

                    if (user.Role == "doctor")
                    {
                        loggedInDoctor = user as Doctor;
                        {
                            Console.WriteLine("User is not a valid doctor.");
                            return;
                        }

                        DoctorMenu();
                        return;
                    }
                }
                else if (user.Role == "student")
                {
                    loggedInStudent = user as Student;
                    if (loggedInStudent == null)
                    {
                        Console.WriteLine("User is not a valid student.");
                        return;
                    }

                    StudentMenu();
                    return;
                }
            }


            Console.WriteLine("Invalid username or password.");
        }

        static void DoctorMenu()
        {
            while (true)
            {
                Console.WriteLine("Doctor Menu:");
                Console.WriteLine("1. Create Course");
                Console.WriteLine("2. View My Courses");
                Console.WriteLine("3. Logout");
                Console.Write("Choose an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        CreateCourse_doctor();
                        break;
                    case "2":
                        ViewMyCourses_doctor();
                        break;
                    case "3":
                        loggedInDoctor = null;
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        static void CreateCourse_doctor()
        {
            Console.Write("Enter course title: ");
            string title = Console.ReadLine();

            Console.Write("Enter course description: ");
            string description = Console.ReadLine();

            Course newCourse = new Course
            {
                Title = title,
                Description = description,
                CreatedBy = loggedInDoctor,
                Assignments = new List<Assignment>(),
                EnrolledStudents = new List<Student>()
            };

            Courses.Add(newCourse);
            Console.WriteLine("Course created successfully.");
        }
        static void ViewMyCourses_doctor()
        {
            if (loggedInDoctor == null)
            {
                Console.WriteLine("No doctor is logged in.");
                return;
            }

            List<Course> myCourses = new List<Course>();

            for (int i = 0; i < Courses.Count; i++)
            {
                Course course = Courses[i];
                if (course.CreatedBy == loggedInDoctor)
                {
                    myCourses.Add(course);
                }
            }

            if (myCourses.Count == 0)
            {
                Console.WriteLine("You have no courses.");
                return;
            }

            Console.WriteLine("Your Courses:");
            for (int i = 0; i < myCourses.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + myCourses[i].Title);
            }

            Console.Write("Choose a course number to manage: ");
            string input = Console.ReadLine();
            int selectedIndex;
            if (!int.TryParse(input, out selectedIndex) || selectedIndex < 1 || selectedIndex > myCourses.Count)
            {
                Console.WriteLine("Invalid choice.");
                return;
            }

            Course selectedCourse = myCourses[selectedIndex - 1];
            CourseViewMenu(selectedCourse);
        }

        static void CourseViewMenu(Course course)
        {
            while (true)
            {
                Console.WriteLine("\n Course View Menu:");
                Console.WriteLine("1. List Assignments");
                Console.WriteLine("2. Create Assignment");
                Console.WriteLine("3. View Assignment");
                Console.WriteLine("4. Back");

                Console.Write("Choose an option: ");
                string option = Console.ReadLine();

                if (option == "1")
                {
                    ListAssignments_doctor(course);
                }
                else if (option == "2")
                {
                    CreateAssignment_doctor(course);
                }
                else if (option == "3")
                {
                    ViewAssignment_doctor(course);
                }
                else if (option == "4")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option.");
                }
            }
        }

        static void ListAssignments_doctor(Course course)
        {
            if (course.Assignments.Count == 0)
            {
                Console.WriteLine("No Assignments available.");
                return;
            }

            Console.WriteLine("Assignments:");
            for (int i = 0; i < course.Assignments.Count; i++)
            {
                Assignment assignment = course.Assignments[i];
                Console.WriteLine((i + 1) + ". " + assignment.Title + " - " + assignment.Description);
            }
        }

        static void CreateAssignment_doctor(Course course)
        {
            Console.Write("Enter assignment title: ");
            string title = Console.ReadLine();

            Console.Write("Enter assignment description: ");
            string description = Console.ReadLine();

            Assignment assignment = new Assignment
            {
                Title = title,
                Description = description,
                Questions = new List<Question>()
            };

            while (true)
            {
                Console.WriteLine("\n Add a new question to the assignment ");
                Console.Write("Enter the question (or type 'done' to finish): ");
                string questionText = Console.ReadLine();
                if (questionText.ToLower() == "done")
                    break;

                Console.Write("Enter the correct answer: ");
                string answer = Console.ReadLine();

                Question question = new Question();
                question.Text = questionText;
                question.Answer = answer;

                assignment.Questions.Add(question);
            }

            course.Assignments.Add(assignment);
            Console.WriteLine("Assignment with questions created successfully.");
        }

        static void ViewAssignment_doctor(Course course)
        {
            if (course.Assignments.Count == 0)
            {
                Console.WriteLine("No assignments in this course.");
                return;
            }

            Console.WriteLine("Assignments:");
            for (int i = 0; i < course.Assignments.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + course.Assignments[i].Title);
            }
            Console.Write("Enter assignment number to view: ");
            int num;
            if (int.TryParse(Console.ReadLine(), out num) && num >= 1 && num <= course.Assignments.Count)
            {
                Assignment selected = course.Assignments[num - 1];

                Console.WriteLine($"Assignment: {selected.Title}");
                Console.WriteLine($"Description: {selected.Description}");

                if (selected.Questions.Count == 0)
                {
                    Console.WriteLine("No questions in this assignment.");
                }
                else
                {
                    Console.WriteLine("Questions:");
                    for (int i = 0; i < selected.Questions.Count; i++)
                    {
                        Console.WriteLine($"Q{i + 1}: {selected.Questions[i].Text} - Answer: {selected.Questions[i].Answer}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }


        }

        static void ShowInfo_doctor()
        {
            if (selectedAssignment == null)
            {
                Console.WriteLine("No assignment selected.");
                return;
            }

            Console.WriteLine("Assignment Info ");
            Console.WriteLine($"Title: {selectedAssignment.Title}");
            Console.WriteLine($"Description: {selectedAssignment.Description}");
            Console.WriteLine($"Due Date: {selectedAssignment.DueDate.ToShortDateString()}");
        }

        static void ListSolutions_doctor()
        {
            if (selectedAssignment == null)
            {
                Console.WriteLine("No assignment selected.");
                return;
            }

            Console.WriteLine("List of Submitted Solutions ");

            if (selectedAssignment.Solutions.Count == 0)
            {
                Console.WriteLine("No solutions submitted yet.");
                return;
            }
        }

        static void GradesReport_doctor()
        {
            if (selectedAssignment == null)
            {
                Console.WriteLine("No assignment selected.");
                return;
            }

            Console.WriteLine("Grades Report");
        }

        static void ViewSolution_doctor()
        {
            if (selectedAssignment == null)
            {
                Console.WriteLine("No assignment selected.");
                return;
            }

            Console.Write("Enter student name: ");
            string name = Console.ReadLine();
        }

        static void StudentMenu()
        {
            if (loggedInStudent == null)
            {
                Console.WriteLine("No student is logged in.");
                return;
            }

            while (true)
            {
                Console.WriteLine(" Student Menu:");
                Console.WriteLine("1. Register in course");
                Console.WriteLine("2. List My Courses");
                Console.WriteLine("3. View a course");
                Console.WriteLine("4. Submit Assignment");
                Console.WriteLine("5. Grades Report");
                Console.WriteLine("6. Logout");

                Console.Write("Choose an option: ");
                string option = Console.ReadLine();


                if (option == "1")
                    RegisterInCourse(loggedInStudent);
                else if (option == "2")
                    ListMyCourses(loggedInStudent);
                else if (option == "3")
                    ViewCourse(loggedInStudent);
                else if (option == "4")
                    SubmitAssignment(loggedInStudent);
                else if (option == "5")
                    GradesReport(loggedInStudent);
                else if (option == "6")
                {
                    loggedInStudent = null;
                    break;
                }
            }

        }
        static void RegisterInCourse(Student student)
        {
            if (student.EnrolledCourses == null)
            {
                student.EnrolledCourses = new List<Course>();
            }

            List<Course> notRegistered = new List<Course>();

            for (int i = 0; i < Courses.Count; i++)
            {
                if (!student.EnrolledCourses.Contains(Courses[i]))
                    notRegistered.Add(Courses[i]);
            }


            if (notRegistered.Count == 0)
            {
                Console.WriteLine("No available courses to register.");
                return;
            }

            Console.WriteLine("Available Courses:");
            for (int i = 0; i < notRegistered.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + notRegistered[i].Title + " (" + notRegistered[i].Code + ")");
            }

            Console.Write("Choose course number: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= notRegistered.Count)
            {
                Course chosenCourse = notRegistered[choice - 1];
                student.EnrolledCourses.Add(chosenCourse);
                Console.WriteLine("Registered in course: " + chosenCourse.Title);
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
        static void SubmitAssignment(Student student)
        {
            if (student.EnrolledCourses.Count == 0)
            {
                Console.WriteLine("You are not enrolled in any courses.");
                return;
            }

            Console.WriteLine("Choose a course to submit an assignment:");
            for (int i = 0; i < student.EnrolledCourses.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + student.EnrolledCourses[i].Title);
            }

            if (!int.TryParse(Console.ReadLine(), out int courseIndex) || courseIndex < 1 || courseIndex > student.EnrolledCourses.Count)
            {
                Console.WriteLine("Invalid course selection.");
                return;
            }

            Course selectedCourse = student.EnrolledCourses[courseIndex - 1];

            if (selectedCourse.Assignments.Count == 0)
            {
                Console.WriteLine("No assignments found in this course.");
                return;
            }

            Console.WriteLine("Choose an assignment to submit:");
            for (int i = 0; i < selectedCourse.Assignments.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + selectedCourse.Assignments[i].Title);
            }

            if (!int.TryParse(Console.ReadLine(), out int assignmentIndex) || assignmentIndex < 1 || assignmentIndex > selectedCourse.Assignments.Count)
            {
                Console.WriteLine("Invalid assignment selection.");
                return;
            }

            Assignment assignment = selectedCourse.Assignments[assignmentIndex - 1];

            if (assignment.Solutions.ContainsKey(student.Username))
            {
                Console.WriteLine("You already submitted this assignment.");
                return;
            }

            int score = 0;
            Dictionary<int, string> studentAnswers = new Dictionary<int, string>();

            for (int i = 0; i < assignment.Questions.Count; i++)
            {
                Question q = assignment.Questions[i];
                Console.WriteLine($"Q{i + 1}: {q.Text}");
                Console.Write("Your answer: ");
                string answer = Console.ReadLine();

                studentAnswers.Add(i, answer);

                if (answer.Trim().ToLower() == q.Answer.Trim().ToLower())
                {
                    score++;
                }
            }

            Solution solution = new Solution
            {
                StudentUsername = student.Username,
                Answers = studentAnswers,
                Grade = score,
                Comment = "Auto-graded"
            };

            assignment.Solutions[student.Username] = solution;
            Console.WriteLine($"Assignment submitted. You scored {score} out of {assignment.Questions.Count}");
        }

        static void ListMyCourses(Student student)
        {
            if (student.EnrolledCourses.Count == 0)
            {
                Console.WriteLine("You are not enrolled in any courses.");
                return;
            }

            Console.WriteLine("Your Courses:");
            for (int i = 0; i < student.EnrolledCourses.Count; i++)
            {
                Course course = student.EnrolledCourses[i];
                Console.WriteLine((i + 1) + ". " + course.Title + " (" + course.Code + ")");
            }
        }

        static void ViewCourse(Student student)
        {

            if (student.EnrolledCourses.Count == 0)
            {
                Console.WriteLine("You are not enrolled in any courses.");
                return;
            }

            Console.WriteLine("Select a course to view:");
            for (int i = 0; i < student.EnrolledCourses.Count; i++)
            {
                Course course = student.EnrolledCourses[i];
                Console.WriteLine((i + 1) + ". " + course.Title);
            }

            Console.Write("Enter course number: ");
            string input = Console.ReadLine();
            int choice;

            if (!int.TryParse(input, out choice))
            {
                Console.WriteLine("Please enter a valid number.");
                return;
            }

            if (choice < 1 || choice > student.EnrolledCourses.Count)
            {
                Console.WriteLine("Number is out of range.");
                return;
            }


            Course selectedCourse = student.EnrolledCourses[choice - 1];


            Console.WriteLine("\nCourse Name: " + selectedCourse.Title);
            Console.WriteLine("Course Code: " + selectedCourse.Code);


            Console.WriteLine("\nAssignments:");
            if (selectedCourse.Assignments.Count == 0)
            {
                Console.WriteLine("No assignments in this course.");
            }
            else
            {
                for (int i = 0; i < selectedCourse.Assignments.Count; i++)
                {
                    Assignment assignment = selectedCourse.Assignments[i];
                    Console.Write(" - " + assignment.Title);


                    if (assignment.Solutions.ContainsKey(student.Username))
                    {
                        Solution solution = assignment.Solutions[student.Username];
                        Console.Write(" | Submitted");

                        if (solution.Grade.HasValue)
                            Console.Write(" | Grade: " + solution.Grade.Value);
                        else
                            Console.Write(" | Not graded yet");
                    }
                    else
                    {
                        Console.Write(" Not submitted");
                    }

                    Console.WriteLine();
                }
            }
        }

        static void GradesReport(Student student)
        {
            if (student.EnrolledCourses.Count == 0)
            {
                Console.WriteLine("You are not enrolled in any courses.");
                return;
            }

            Console.WriteLine("Grades Report:");
            for (int i = 0; i < student.EnrolledCourses.Count; i++)
            {
                Course course = student.EnrolledCourses[i];
                int total = 0;
                int gradedCount = 0;

                for (int j = 0; j < course.Assignments.Count; j++)
                {
                    Assignment assignment = course.Assignments[j];
                    if (assignment.Solutions.TryGetValue(student.Username, out Solution solution))
                    {


                        if (assignment.Grade.HasValue)
                        {
                            total += assignment.Grade.Value;
                            gradedCount++;
                        }
                    }
                }

                Console.Write(course.Title + " (" + course.Code + "): ");
                if (gradedCount == 0)
                {
                    Console.WriteLine("No graded assignments.");
                }
                else
                {
                    Console.WriteLine("Total Grade = " + total);
                }
            }
        }
    }
}









