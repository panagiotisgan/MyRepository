using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise4_Ver2
{
    public class WorkerManager
    {
        public void CreateWorker(string firstName,string lastName,float salary,int dailyHours)
        {
            string newFName = firstName.First().ToString().ToUpper() + firstName.Substring(1);
            string newLName = lastName.First().ToString().ToUpper() + lastName.Substring(1);

            Worker worker = new Worker()
            {
                FirstName = newFName,
                LastName = newLName,
                WeekSalary = salary,
                WorkHoursDay = dailyHours
            };

            using (var db = new App_Context())
            {
                db.Workers.Add(worker);
                db.SaveChanges();
            }
        }

        public void CalculateHourlyPayment(int workerId)
        {
            List<Worker> workers;
            using (var db = new App_Context())
            {
                workers = db.Workers.Where(w => w.Id == workerId).ToList();
            }

            foreach (var item in workers)
            {
                float x = item.WeekSalary / (5*item.WorkHoursDay);
                Console.WriteLine($"The hourly payment of employee {item.LastName} it's {x}.");
            }
            
        }

        public void GetWorkerDetails()
        {
            List<Worker> work;

            using (var db = new App_Context())
            {
                work = db.Workers.ToList();
                foreach (var w in work)
                {
                    float x = w.WeekSalary / (5 * w.WorkHoursDay);
                    String s = String.Format("Salary per hour:{0:0.00}",x);
                    Console.WriteLine($"First Name: {w.FirstName}");
                    Console.WriteLine($"Last Name: {w.LastName}");
                    Console.WriteLine($"Week Salary: {w.WeekSalary}");
                    Console.WriteLine($"Working hours per day: {w.WorkHoursDay}");
                    Console.WriteLine(s);
                }
            }
        }
    }
}
