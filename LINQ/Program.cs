using System;
using System.Collections.Generic;
using System.Linq;
namespace LINQ
{
    public class Student{ 
        public int Id { get; set; }
        public string Namn { get; set; } = ""; 
        public int Alder { get; set; }
        public string Klass { get; set; } = ""; 
        public int Poang { get; set; } 
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Exempeldata
            List<Student> studenter = new List<Student> 
            { 
                new Student { Id = 1, Namn = "Anna", Alder = 19, Klass = "A", Poang = 85 }, 
                new Student { Id = 2, Namn = "Bertil", Alder = 22, Klass = "B", Poang = 72 }, 
                new Student { Id = 3, Namn = "Cecilia", Alder = 20, Klass = "A", Poang = 91 }, 
                new Student { Id = 4, Namn = "David", Alder = 24, Klass = "C", Poang = 64 }, 
                new Student { Id = 5, Namn = "Eva", Alder = 19, Klass = "B", Poang = 85 }, 
                new Student { Id = 6, Namn = "Felix", Alder = 21, Klass = "A", Poang = 77 }, 
                new Student { Id = 7, Namn = "Gustav", Alder = 23, Klass = "C", Poang = 58 }, 
                new Student { Id = 8, Namn = "Hanna", Alder = 20, Klass = "B", Poang = 95 } 
            };

            Console.WriteLine("LINQ-ÖVNINGAR MED STUDENTER\n");



            // Hämta alla studenter som är äldre än 20
            Console.WriteLine();
            Console.WriteLine("Studenter äldre än 20 år: ");
            List<Student> query = new();
            query = (from student in studenter
                     where student.Alder > 20
                     select student).ToList(); // .ToList() converts the result of the query to a list. Needed to convert from IEnumerable to a List. W.o it the program crashes

            foreach(Student student in query)
            {
                Console.WriteLine($"Namn: {student.Namn} | Ålder: {student.Alder}");
            }


            // Hämta alla studenter som har mer än 80 poäng
            Console.WriteLine();
            Console.WriteLine("Studenter som har mer än 80 poäng: ");
            List<Student> eightyPoints = new();
            eightyPoints = (from student in studenter
                            where student.Poang > 80
                            select student).ToList();

            foreach(Student student in eightyPoints)
            {
                Console.WriteLine($"Namn: {student.Namn} | Poäng: {student.Poang}");
            }



            // Visa bara namnen på alla studenter
            Console.WriteLine();
            Console.WriteLine("--- Namnlista ---");
            List<string> names = new();
            names = (from student in studenter                     
                     select student.Namn).ToList();

            foreach(string student in names)
            {
                Console.WriteLine(student);
            }

            // Visa namn och poäng i en anonym typ. Föreläsningen "LINQ": anonyma typer används för att forma ett nytt resultat on-the-fly
            Console.WriteLine();
            Console.WriteLine("--- Namnlista & Poäng (anonym typ) ---");
            var result = from student in studenter 
                         select new { student.Namn, student.Poang };

            foreach(var student in result)
            {
                Console.WriteLine($"Namn: {student.Namn} - Poäng: {student.Poang}");
            }


            // Ordna studenter efter ålder
            Console.WriteLine();
            Console.WriteLine("--- Namnlista & Poäng (sorterat på ålder) ---");
            var orderAge = from student in studenter
                           orderby student.Alder descending
                           select new { student.Namn, student.Alder };

            foreach (var student in orderAge)
            {
                Console.WriteLine($"Namn: {student.Namn} - Ålder: {student.Alder}");
            }


            // Ordna efter poäng, högst först
            Console.WriteLine();
            Console.WriteLine("--- Namnlista & Poäng (sorterat på poäng) ---");
            var orderScore = from student in studenter
                           orderby student.Poang descending
                           select new { student.Namn, student.Poang };

            foreach (var student in orderScore)
            {
                Console.WriteLine($"Namn: {student.Namn} - Poäng: {student.Poang}");
            }


            // Räkna antalet studenter
            Console.WriteLine();
            Console.WriteLine("--- Antal studenter ---");
            List<string> numStudents = new();
            numStudents = (from student in studenter
                     select student.Namn).ToList();

            Console.WriteLine(numStudents.Count());


            // Antalet studenter i klass A
            Console.WriteLine();
            Console.WriteLine("--- Antal studenter i klass A ---");
            List<Student> classAStudents = new();
            classAStudents = (from student in studenter
                              where student.Klass=="A"
                           select student).ToList();

            Console.WriteLine(classAStudents.Count());


            // Ta ut de 3 första studenterna
            Console.WriteLine();
            Console.WriteLine("--- De tre första studenterna ---");
            List<Student> firstThreeStudents = new();
            firstThreeStudents = (from student in studenter
                                  select student).Take(3).ToList();

            foreach(Student student in firstThreeStudents)
            {
                Console.WriteLine($"Namn: {student.Namn}");
            }



            // Ordna efter poäng, högst först och plocka ut de två första
            Console.WriteLine();
            Console.WriteLine("--- Namnlista & Poäng (topp 2) ---");
            var orderScoreFirstTwo = from student in studenter
                             orderby student.Poang descending
                             select new { student.Namn, student.Poang };

            foreach (var student in orderScoreFirstTwo.Take(2))
            {
                Console.WriteLine($"Namn: {student.Namn} - Poäng: {student.Poang}");
            }


            // Visa distinkta klassnamn
            Console.WriteLine();
            Console.WriteLine("--- Unika klassnamn ---");
            List<string> distinctClasses = new();
            distinctClasses = (from klass in studenter                               
                                  select klass.Klass).Distinct().ToList();

            foreach (string klassNamn in distinctClasses)
            {
                Console.WriteLine($"Klass: {klassNamn}");
            }


            // Visa distinkta åldrar
            Console.WriteLine();
            Console.WriteLine("--- Unika åldrar ---");
            List<int> distinctAges = new();
            distinctAges = (from student in studenter
                               select student.Alder).Distinct().ToList();

            foreach (int alder in distinctAges)
            {
                Console.WriteLine($"Klass: {alder}");
            }


            // Se om någon student har 100 poänd
            Console.WriteLine();
            Console.WriteLine("--- Studenter med 100 poäng ---");
            bool perfectScore;
            perfectScore = (from student in studenter
                            select student.Poang).Any(x => x == 100); // Ett lambdauttryck! Nu eftersom perfectScore är en bool och inte en List som tidigare så behövdes inte .ToList();

            Console.WriteLine($"Finns någon student med 100 poäng? Svar: {perfectScore}");


            // Har alla minst 50 poäng?
            Console.WriteLine();
            Console.WriteLine("--- Studenter med 50 poäng ---");
            bool fifty;
            fifty = (from student in studenter
                    select student.Poang).All(x => x >= 50);

            Console.WriteLine($"Har alla studenter minst 50 poäng? Svar: {fifty}");


            // Första studenten i klass B
            Console.WriteLine();
            Console.WriteLine("--- Första studenten i klass B ---");
            string firstStudentClassB = (from student in studenter
                                  where student.Klass == "B"
                                  select student.Namn).FirstOrDefault();

            Console.WriteLine(firstStudentClassB);


            // Sista studenten i listan
            Console.WriteLine();
            Console.WriteLine("--- Sista studenten i listan ---");
            string lastStudent = (from student in studenter
                                  select student.Namn).Last();

            Console.WriteLine(lastStudent);

            // Hämta ut student med ID 2
            Console.WriteLine();
            Console.WriteLine("--- Student med ID 2 ---");
            string studentId = (from student in studenter
                             where student.Id == 2
                                select student.Namn).Single();

            Console.WriteLine(studentId);


            // Gruppera studenter per klass
            /*Console.WriteLine();
            Console.WriteLine("--- Gruppera studenter per klass ---");
            List<Student> klassgrupp = new();
            klassgrupp = (from student in studenter
                          select student).GroupBy(student.Klass).ToList();*/
        }
    }
}