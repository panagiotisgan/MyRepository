using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise4_Ver2
{
    public class StudentManager
    {
        public Student CreateStudent(string firstName,string lastName,string facuNamber)
        {
            string newFName = firstName.First().ToString().ToUpper() + firstName.Substring(1);
            string newLName = lastName.First().ToString().ToUpper() + lastName.Substring(1);

            Student student = new Student() { FirstName = newFName , LastName = newLName, FacultyNumber = facuNamber };

            using(var db = new App_Context())
            {
                db.Students.Add(student);
                db.SaveChanges();
            }

            return student;
        }

        public void StudentData()
        {
            List<Student> students;

            using(var db = new App_Context())
            {
                students = db.Students.ToList();
                foreach (var student in students)
                {
                    Console.WriteLine($"First Name: {student.FirstName}");
                    Console.WriteLine($"Last Name: {student.LastName}");
                    Console.WriteLine($"Faculty Number: {student.FacultyNumber}");
                }
            }
        }
    }
}
