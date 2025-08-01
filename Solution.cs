using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace Educational_Management_System
    {
    class Solution
    {
        public string StudentUsername { get; set; }
        public string Text { get; set; }
        public int? Grade { get; set; } = null;
        public string Comment { get; set; }
        public Dictionary<int, string> Answers { get; set; } = new Dictionary<int, string>();
    
    }



}


