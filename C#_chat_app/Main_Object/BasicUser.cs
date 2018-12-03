using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Main_Object
{
    public class BasicUser:SimpleUser
    {
        

        public void ViewData()
        {
            using (var db = new App_Context())
            {
                var allMessages = db.Messages.ToList();

                foreach (var item in allMessages)
                {
                    Console.WriteLine($"Message id:{item.Id}"+$"\nMessage:{item.Message}"+$"\nSender:{item.SenderName}"+$"\nReceiver:{item.ReceiverName}"+$"\nShipping date:{item.DateTime}\n");
                }

            }
            
        }
    }
}
