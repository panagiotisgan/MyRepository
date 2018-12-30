using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Main_Object.Menus;

namespace Main_Object
{
    public class OptionsMenu
    {
        private readonly string emailPattern = @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$";
        /*Passwords with min 8 - max 15 character length, at least two letters (not case sensitive), 
         * one number, one special character (all, not just defined), space is not allowed.*/
        private readonly string passPattern = @"^(?=(.*[a-zA-Z].*){2,})(?=.*\d.*)(?=.*\W.*)[a-zA-Z0-9\S]{8,15}$";

        static string CreateUser(string elem, string elem_2)
        {
            Console.Write($"{elem} your {elem_2}: ");
            string num = Console.ReadLine();
            return num;
        }

        static bool ValidAccessId(string id)
        {
            if (id.Equals("1") || id.Equals("2") || id.Equals("3") || id.Equals("4"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public OptionsMenu()
        {
            AdminUser manager = new AdminUser();

            bool firstChoise = true;
            int select;
            while (firstChoise)
            {
                bool correctNum = false;
                do { 
                    Console.WriteLine("1.Application Login\n2.Login As Super Admin\n3.Create Account\n0.Close Application");
                    Console.WriteLine("=================");
                    Console.WriteLine("Select from Menu:");


                    
                    bool isNum = int.TryParse(Console.ReadLine(), out select);
                    if (select < 0 || select > 3)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Your choose it's out of range.");
                        Console.ResetColor();
                        break;
                    }
                    else
                    {
                        correctNum = true;
                    }
                } while (!correctNum);

                switch (select)
                {
                    case 1:
                        //This bool become true only if username and pass it's correct
                        //otherwise still ask insert username and pass
                        bool c = false;

                        do
                        {
                            string user = CreateUser("Give", "username");
                            string pass = CreateUser("Give", "password");
                            AccessId x = manager.Login(user, pass);
                            if (x != 0 && x!=AccessId.Admin)
                            {
                                Console.WriteLine($"Welcome {user}");

                                switch (x)
                                {
                                    case AccessId.Simple:
                                        SimpleMenu simpleMenu = new SimpleMenu(user);
                                        break;
                                    case AccessId.Basic:
                                        BasicMenu basicMenu = new BasicMenu(user);
                                        break;
                                    case AccessId.Edit:
                                        EditMenu editMenu = new EditMenu(user);
                                        break;
                                    case AccessId.Delete:
                                        DeleteMenu deleteMenu = new DeleteMenu(user);
                                        break;
                                }
                                c = true;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("Your username or password it's incorrect.Try again.");
                                Console.ResetColor();
                            }
                        } while (!c);
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Admin Login Form"));
                        string userAdmin = CreateUser("Give", "username");
                        string password = CreateUser("Give", "password");
                        AccessId adm = manager.Login(userAdmin, password);
                        if (adm == AccessId.Admin)
                        {
                            Console.WriteLine($"Welcome {userAdmin}\n");
                            bool take = false;
                            do
                            {
                                Console.Clear();
                                Console.WriteLine("1.Create a user\n2.View the user of system\n3.Delete a user\n4.Update a user\n0.Log Out");
                                int val;
                                string email;
                                bool n = int.TryParse(Console.ReadLine(), out val);
                                switch (val)
                                {
                                    case 1:
                                        try
                                        {
                                            userAdmin = CreateUser("Give", "username");
                                            password = CreateUser("Give", "password");
                                            email = CreateUser("Give", "email");
                                            manager.Register(userAdmin, password, email);
                                        }
                                        catch(Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                            Console.WriteLine("Press any button to exit.");
                                            Console.ReadKey();
                                            return;
                                        }
                                        
                                        break;
                                    case 2:
                                        manager.ViewUser();
                                        break;
                                    case 3:
                                        try
                                        {
                                            int userId;
                                            bool id = int.TryParse(CreateUser("Give", "user id"), out userId);
                                            User a = new User() { Id = userId};
                                            manager.DeleteUser(a);
                                        }
                                        catch(Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                            Console.WriteLine("Press any button to exit.");
                                            Console.ReadKey();
                                            return;
                                        }
                                        break;
                                    case 4:
                                        Console.WriteLine("1.Change Password\n2.Change Access id\n3.Change email\n4.Change Password and Access Id" +
                                            "\n5.Change Email and Access Id\n6.Change Password,Email and Access Id");
                                        int takeVal,_id;
                                        bool _take = int.TryParse(Console.ReadLine(),out takeVal);
                                        string success,_email;
                                        AccessId e;
                                        switch (takeVal)
                                        {
                                            case 1:
                                                try
                                                {
                                                    _take = int.TryParse(CreateUser("Give", "user id"), out _id);
                                                    password = CreateUser("Give", "password");
                                                    manager.UpdateUser(_id, password);
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
                                                try
                                                {
                                                    bool _successBool = false;
                                                    _take = int.TryParse(CreateUser("Give", "user id"), out _id);
                                                    do
                                                    {
                                                        success = CreateUser("Give", "access id");
                                                        if (!ValidAccessId(success))
                                                            Console.WriteLine("Not valid access id");
                                                        else
                                                        _successBool = true;
                                                    } while (!_successBool);
                                                    e = (AccessId)Enum.Parse(typeof(AccessId), success);
                                                    manager.UpdateUser(_id, e);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                    Console.WriteLine("Press any button to exit.");
                                                    Console.ReadKey();
                                                    return;
                                                }
                                                
                                                break;
                                            case 3:
                                                try
                                                {
                                                    _take = int.TryParse(CreateUser("Give", "user id"), out _id);
                                                    _email = CreateUser("Give", "email");
                                                    manager.UpdateEmail(_id, _email);
                                                }
                                                catch(Exception ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                    Console.WriteLine("Press any button to exit.");
                                                    Console.ReadKey();
                                                    return;
                                                }
                                                
                                                break;
                                            case 4:
                                                try
                                                {
                                                    bool _successBool = false;
                                                    _take = int.TryParse(CreateUser("Give", "user id"), out _id);
                                                    password = CreateUser("Give", "password");
                                                    do
                                                    {
                                                        success = CreateUser("Give", "access id");
                                                        if (!ValidAccessId(success))
                                                            Console.WriteLine("Not valid access id");
                                                        else
                                                            _successBool = true;
                                                    } while (!_successBool);
                                                    e = (AccessId)Enum.Parse(typeof(AccessId), success);
                                                    manager.UpdateUser(_id, password, e);
                                                }
                                                catch(Exception ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                    Console.WriteLine("Press any button to exit.");
                                                    Console.ReadKey();
                                                    return;
                                                }
                                                break;
                                            case 5:
                                                try
                                                {
                                                    bool _successBool = false;
                                                    _take = int.TryParse(CreateUser("Give", "user id"), out _id);
                                                    _email = CreateUser("Give", "email");
                                                    do
                                                    {
                                                        success = CreateUser("Give", "access id");
                                                        if (!ValidAccessId(success))
                                                            Console.WriteLine("Not valid access id");
                                                        else
                                                            _successBool = true;
                                                    } while (!_successBool);
                                                    e = (AccessId)Enum.Parse(typeof(AccessId), success);
                                                    manager.UpdateEmail(_id, _email, e);
                                                }
                                                catch(Exception ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                    Console.WriteLine("Press any button to exit.");
                                                    Console.ReadKey();
                                                    return;
                                                }
                                                break;
                                            case 6:
                                                try
                                                {
                                                    bool _successBool = false;
                                                    _take = int.TryParse(CreateUser("Give", "user id"), out _id);
                                                    password = CreateUser("Give", "password");
                                                    _email = CreateUser("Give", "email");
                                                    do
                                                    {
                                                        success = CreateUser("Give", "access id");
                                                        if (!ValidAccessId(success))
                                                            Console.WriteLine("Not valid access id");
                                                        else
                                                            _successBool = true;
                                                    } while (!_successBool);
                                                    e = (AccessId)Enum.Parse(typeof(AccessId), success);
                                                    manager.UpdateUser(_id, _email, password, e);
                                                }
                                                catch(Exception ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                    Console.WriteLine("Press any button to exit.");
                                                    Console.ReadKey();
                                                    return;
                                                }
                                                break;
                                        }
                                        break;
                                     case 0:
                                        Environment.Exit(0);
                                        break;
                                }

                                Console.WriteLine("Do you want to continue?(Y/N)");
                                string answer = Console.ReadLine();

                                if (answer == "Y" || answer == "y")
                                {
                                    take = false;
                                }
                                else
                                {
                                    take = true;
                                    manager.Logout();
                                }
                            } while (!take);
                        }
                        else
                        {
                            Console.WriteLine("You enter invalid details!");
                            Environment.Exit(0);
                        }

                        break;
                    case 3:
                        string usernameOne,passOne,emailOne;
                        bool first = false;
                        bool second = false;
                        bool third = false;

                        do
                        {
                            usernameOne = CreateUser("Give", "username").Trim();
                            var x = manager.ExistUser(usernameOne);
                            if (!x)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine("The username already exist.");
                                Console.ResetColor();
                            }
                            else if (usernameOne == "" || usernameOne.Length < 6 || usernameOne.Length > 12 || !x)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine("The username must be between 6 and 12 characters.");
                                Console.ResetColor();
                                first = false;
                            }
                            else
                            {
                                first = true;
                            }

                        } while (!first);

                        do
                        {
                            passOne = CreateUser("Give", "password");
                            bool isValidPass = Regex.IsMatch(passOne, passPattern);
                            if (!isValidPass)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine("The password must contain between 8 and 15 characters." +
                                    "Also must contain 2 letters,one special character and one number.");
                                Console.ResetColor();
                                second = false;
                            }
                            else
                            {
                                second = true;
                            }

                        } while (!second);

                        do
                        {
                            emailOne = CreateUser("Give", "email").Trim();
                            bool isValidEmail = Regex.IsMatch(emailOne, emailPattern);
                            if(!isValidEmail)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine("The email doesn't have the correct format.");
                                Console.ResetColor();
                            }
                            else {
                                third = true;
                            }
                        } while (!third);

                        try
                        {
                            bool p = manager.Register(usernameOne, passOne, emailOne);
                            if (p)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine("Your account create succesfully.");
                                Console.ResetColor();
                                Console.WriteLine($"Your username: {usernameOne}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("Press any button to exit.");
                            Console.ReadKey();
                            return;
                        }

                        Console.WriteLine("Do you want to continue? Y/N");
                        string yourChoose = Console.ReadLine();
                        if (yourChoose == "Y" || yourChoose == "y")
                        {
                            firstChoise = true;
                        }
                        else
                        {
                            firstChoise = false;
                            manager.Logout();
                        }
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                }
            }
            Console.ReadKey();
        }
    }
}
