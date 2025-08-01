using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educational_Management_System;

namespace Educational_Management_System
{
    class Student : User
    {
        public List<Course> EnrolledCourses = new List<Course>();
        public Dictionary<Assignment, bool> Submissions = new Dictionary<Assignment, bool>();
    }
}


