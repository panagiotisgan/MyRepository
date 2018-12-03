using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Object
{
    public class SimpleUser
    {
        private string path = "Messages" + Guid.NewGuid() + ".txt";

        public void CreateMessage(string message, string sender, string receiver)
        {

            FileAccess fileAccess = new FileAccess(path);

            fileAccess.WriteMessage(sender, receiver, message);

            using (var db = new App_Context())
            {
                IList<User> sendDetails = db.Users.Where(i => i.Username == sender).ToList();
                IList<User> recevDetails = db.Users.Where(r => r.Username == receiver).ToList();

                if (recevDetails.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("The recipient you choose does not exist.");
                    Console.ResetColor();
                }
                else
                {
                    foreach (User item in sendDetails)
                    {

                        foreach (User item_2 in recevDetails)
                        {
                            MessageDTO messageDTO = new MessageDTO
                            {
                                Message = message,
                                SenderName = sender,
                                ReceiverName = receiver,
                                DateTime = DateTime.Now,
                                Sender = item,
                                Receiver = item_2
                            };

                            db.Messages.Add(messageDTO);
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("The message was send it successfully.");
                            Console.ResetColor();
                            db.SaveChanges();
                        }
                    }
                }

            }
        }

        public void ViewMyMessage(string username)
        {
            using (var db = new App_Context())
            {
                var myMessage = db.Messages.Where(i => i.ReceiverName == username).Select(m => new { m.Message, m.SenderName }).ToList();

                if (myMessage.Count > 0)
                {
                    Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "You have the following messages"));
                    foreach (var item in myMessage)
                    {
                        Console.WriteLine($"{item.SenderName} - {item.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("You have not any massage in your mail box.");
                }

            }
        }

        public int NumberOfMess(string username)
        {
            using (var db = new App_Context())
            {
                var mess = db.Messages.Where(m => m.ReceiverName == username).ToList();
                if (mess.Count > 0)
                {
                    int number = mess.Count;
                    return number;
                }

            }
            return 0;
        }
    }
}
