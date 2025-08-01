using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational_Management_System
{
    class Course
    {
        public string Title;
        public string Description;
        public string Name;
        public string Code;
        public Doctor CreatedBy;
        public List<Student> EnrolledStudents = new List<Student>();
        public List<Assignment> Assignments = new List<Assignment>();

    }
}
