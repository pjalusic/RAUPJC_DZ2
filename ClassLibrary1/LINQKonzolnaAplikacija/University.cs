using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQKonzolnaAplikacija
{
    class University
    {
        public string Name { get; set; }
        public Student[] Students { get; set; }

        public University(Student[] students)
        {
            Students = students;
        }
    }
}
