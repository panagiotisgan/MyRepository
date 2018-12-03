using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Main_Object
{
    public class EditUser : BasicUser
    {

        public void EditMessage(int messId, string message)
        {

            using (var db = new App_Context())
            {
                

                var _messId = db.Messages.Find(messId);

                if (_messId != null)
                {
                    _messId.Message = message;
                    _messId.DateTime = DateTime.Now;
                    db.SaveChanges();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Update succesfully!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Update Failed.");
                    Console.ResetColor();
                }
            }
        }
    }
}
