using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQKonzolnaAplikacija
{
    class Program
    {
        static void Main(string[] args)
        {
            // 3. ZADATAK
            int[] integers = new[] { 1, 2, 2, 2, 3, 3, 4, 5 };
            string[] strings = integers.OrderBy(i => i)
                .GroupBy(i => i)
                .Select(i => $"Broj {i.Average()} ponavlja se {i.Count()} puta")
                .ToArray();
            foreach (var s in strings)
            {
                Console.WriteLine(s);
            }

            // 4. ZADATAK
            Example1();
            Example2();

            // 5. ZADATAK
            University[] universities = GetAllCroatianUniversities();

            Student[] allCroatianStudents = (from uni in universities
                from s in uni.Students
                    select s).Distinct().ToArray();

            Student[] croatianStudentsOnMultipleUniversities = (from uni in universities
            from s in uni.Students
            group s by s
            into studs
            where studs.Count() > 1
            from addStud in studs
            select addStud).Distinct().ToArray();

            var studentsOnMaleOnlyUniversities = universities.Where(uni => uni.Students.All(s => s.Gender == Gender.Male))
                .SelectMany(uni => uni.Students.Select(s => s).ToArray())
                .ToArray();
                                                        

            Print(allCroatianStudents);
            Print(croatianStudentsOnMultipleUniversities);
            Print(studentsOnMaleOnlyUniversities);


            Console.ReadLine();
        }

        static University[] GetAllCroatianUniversities()
        {
            var s1 = new Student("Ivan", jmbag: "001234567");
            s1.Gender = Gender.Male;
            var s2 = new Student("Jurica", jmbag: "001334568");
            s2.Gender = Gender.Male;
            var s3 = new Student("Ivica", jmbag: "001234557");
            s3.Gender = Gender.Male;

            var s4 = new Student("Marica", jmbag: "004234568");
            s4.Gender = Gender.Female;
            var s5 = new Student("Klarica", jmbag: "004234557");
            s5.Gender = Gender.Female;

            var list1 = new List<Student>() { s1, s2, s3 };
            var list2 = new List<Student>() { s1, s4, s2, s5 };

            return new[]
            {
                new University(list1.ToArray()),
                new University(list2.ToArray()),
            };
        }

        static void Print(Student[] students)
        {
            Console.WriteLine();
            foreach (var s in students)
            {
                Console.WriteLine("{0}: {1}", s.v, s.jmbag);
            }
        }

        static void Example1()
        {
            var list = new List<Student>()
            {
                new Student ("Ivan", jmbag :"001234567")
            };
            var ivan = new Student("Ivan", jmbag: "001234567");

            // false :(
            bool anyIvanExists = list.Any(s => s == ivan);
            Console.WriteLine("\nIvan{0} postoji.", anyIvanExists? "" : " ne");
        }

        static void Example2()
        {
            var list = new List<Student>()
            {
                new Student ("Ivan", jmbag :"001234567") ,
                new Student ("Ivan", jmbag :"001234567")
            };

            // 2 :(
            var distinctStudents = list.Distinct().Count();
            Console.WriteLine("Broj različitih studenata u listi: {0}", distinctStudents);
        }
    }
}
