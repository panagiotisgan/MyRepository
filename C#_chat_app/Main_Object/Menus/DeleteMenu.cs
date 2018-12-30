using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Object.Menus
{
    public class DeleteMenu
    {
        protected string CreateUser(string elem, string elem_2)
        {
            Console.Write($"{elem} your {elem_2}: ");
            string num = Console.ReadLine();
            return num;
        }

        public DeleteMenu(string user)
        {
            DeleteUser delete = new DeleteUser();
            bool take = false;
            string mess;
            do
            {
                Console.Clear();
                int number = delete.NumberOfMess(user);
                Console.WriteLine($"You have {number} message in you message box.");
                Console.WriteLine("1.Press one to send a message\n2.Press two to view all messages " +
                   "\n3.Press three to view your message\n4.Press four to update messages\n5.Press five to delete a message\n0.Log Out");
                int choose;
                bool isCor = int.TryParse(Console.ReadLine(), out choose);
                switch (choose)
                {
                    case 1:
                        try
                        {
                            do
                            {
                                mess = CreateUser("Write", "message");
                                if (mess.Length > 250)
                                {
                                    Console.WriteLine("You pass the maximum of 250 characters!");
                                }
                            } while (mess.Length > 250);
                            string receiver = CreateUser("Give", "recipient");
                            delete.CreateMessage(mess, user, receiver);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("Press any button to exit.");
                            Console.ReadKey();
                            return;
                        }
                        break;
                    case 2:
                        delete.ViewData();
                        break;
                    case 3:
                        delete.ViewMyMessage(user);
                        break;
                    case 4:
                        try
                        {
                            int messId;
                            bool id = int.TryParse(CreateUser("Give", "message id"), out messId);
                            do
                            {
                                mess = CreateUser("Edit", "message");
                                if (mess.Length > 250)
                                {
                                    Console.WriteLine("You pass the maximum of 250 characters!");
                                }
                            } while (mess.Length > 250);
                            delete.EditMessage(messId, mess);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("Press any button to return.");
                            Console.ReadKey();
                            return;
                        }
                        break;
                    case 5:
                        try
                        {
                            int messaId;
                            bool delMess = int.TryParse(CreateUser("Give", "message id"), out messaId);
                            delete.DeleteMessage(messaId);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("Press any button to exit.");
                            Console.ReadKey();
                            return;
                        }
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                }

                Console.WriteLine("Do you want to continue?(Y/N)");
                string yn = Console.ReadLine();

                if (yn == "Y" || yn == "y")
                {
                    take = false;
                }
                else
                {
                    take = true;
                    Environment.Exit(0);
                }

            } while (!take);
        }
    }
}
