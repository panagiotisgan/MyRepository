using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Object
{
    public class DeleteUser : EditUser
    {

        public void DeleteMessage(int messageId)
        {
            bool value = false;

            using (var db = new App_Context())
            {
                var message = db.Messages.Find(messageId);

                if (message != null)
                {
                    value = true;
                }

                if (value)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("The message will delete permanently.Do you want to continue? Y/N");
                    //Console.ResetColor();
                    string x = Console.ReadLine();
                    if (x == "Y" || x == "y")
                    {
                        db.Messages.Remove(message);
                        db.SaveChanges();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("The message delete it.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Deleting aborted.");
                        Console.ResetColor();
                    }

                }
            }
        }
    }
}
