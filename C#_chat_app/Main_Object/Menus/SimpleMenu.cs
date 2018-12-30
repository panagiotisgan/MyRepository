using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Object.Menus
{
    public class SimpleMenu
    {
        protected string CreateUser(string elem, string elem_2)
        {
            Console.Write($"{elem} your {elem_2}: ");
            string num = Console.ReadLine();
            return num;
        }

        public SimpleMenu(string user)
        {
            SimpleUser simpleUser = new SimpleUser();
            int choise;
            string mess;
            bool take = false;

            do
            {
                Console.Clear();
                int number = simpleUser.NumberOfMess(user);
                Console.WriteLine($"You have {number} message in you message box.");
                Console.WriteLine("1.Press one to send a message\n2.Press two to view your message.\n0.Log Out");
                bool num = int.TryParse(Console.ReadLine(), out choise);

                switch (choise)
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
                            simpleUser.CreateMessage(mess, user, receiver);
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
                        simpleUser.ViewMyMessage(user);
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
