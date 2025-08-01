using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational_Management_System
{
    class Assignment
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Grade { get; set; }
        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(7); 
        public Dictionary<string, Solution> Solutions { get; set; } = new Dictionary<string, Solution>();
        public List<Question> Questions { get; set; } = new List<Question>();
    }



}

