using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Exercise4_Ver2
{
    public class Program
    {
        

        static void Main(string[] args)
        {

            StudentManager student = new StudentManager();
            WorkerManager worker = new WorkerManager();
            Console.WriteLine("1.To create a new student.\n2.To create a new worker.\n3.Get Students Details.\n4.Get workers details.");
            string choise = Console.ReadLine();
            string match = @"^[A-Z0-9]{2}$";
            switch (choise)
            {
                
                case "1":
                    bool value = true;
                    string name, last, fuc;
                    do
                    {
                        Console.WriteLine("Give the first name: ");
                        name = Console.ReadLine();
                        Console.WriteLine("Give the last name: ");
                        last = Console.ReadLine();
                        Console.WriteLine("Give the Faculty number: ");
                        fuc = Console.ReadLine();
                        bool isValidFucalty = Regex.IsMatch(fuc,match);
                        if (name.Length < 2 || last.Length < 3 || !isValidFucalty)
                        {
                            Console.WriteLine("Your first name or lastname it is not correct.");
                            value = true;
                        }
                        else
                            value = false;

                    } while (value);
                    student.CreateStudent(name, last, fuc);
                    break;
                case "2":
                    bool value_1 = true;
                    float weeksalary;
                    int workingPerDay;
                    do
                    {
                        Console.WriteLine("Give the first name: ");
                        name = Console.ReadLine();
                        Console.WriteLine("Give the last name: ");
                        last = Console.ReadLine();
                        Console.WriteLine("Give the Week Salary: ");
                        weeksalary = float.Parse(Console.ReadLine());
                        Console.WriteLine("Give working hours per day: ");
                        workingPerDay = int.Parse(Console.ReadLine());

                        if (name.Length < 2 || last.Length < 3 || weeksalary < 10.0 || (workingPerDay < 1 || workingPerDay > 12))
                        {
                            Console.WriteLine("Your data it's incorrect.");
                            value_1 = true;
                        }
                        else
                            value_1 = false;
                    } while (value_1);
                    worker.CreateWorker(name, last, weeksalary, workingPerDay);
                    break;
                case "3":
                    student.StudentData();
                    break;
                case "4":
                    worker.GetWorkerDetails();
                    break;
            }

            //student.StudentData();
            Console.ReadKey();
        }
    }
}
