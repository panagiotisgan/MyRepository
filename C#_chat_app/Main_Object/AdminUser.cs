using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Main_Object
{
    public class AdminUser
    {
        private string adminFile = "LogFile.txt";

        public bool ExistUser(string username)
        {
            using (var db = new App_Context())
            {
                var result = db.Users.Where(i => i.Username == username).SingleOrDefault();
                if(result==null)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Register(string username, string password, string email)
        {
            FileAccess access = new FileAccess(adminFile);

            using (var db = new App_Context())
            {
                var isNull = ExistUser(username);

                if (isNull)
                {
                    var salt = Password.GetSalt();
                    var hash = Password.Hash(password, salt);

                    User user = new User {
                        Username = username,
                        Password = Convert.ToBase64String(hash),
                        Salt = Convert.ToBase64String(salt),
                        Email = email,
                        AccessId = AccessId.Simple
                    };

                    db.Users.Add(user);
                    db.SaveChanges();

                    access.WriteLogFile(username, password, email);
                    return true;
                }
            }
            return false;
        }

        public AccessId Login(string username, string password)
        {
            FileAccess access = new FileAccess(adminFile);

            using (var db = new App_Context())
            {
                
                var salt = db.Users.Where(i => i.Username == username).Select(p => p.Salt).SingleOrDefault();

                byte[] hash;
                
                if (salt!=null)
                {
                     hash = Password.Hash(password, Convert.FromBase64String(salt));
                     var dbpass = Convert.ToBase64String(hash);

                    var result = db.Users.Where(u => u.Username == username && u.Password == dbpass).ToList();

                    if (result.Count != 0) {
                        foreach (User item in result)
                        {
                            access.WriteLogFile(username, password);
                            return item.AccessId;
                        }
                    }
                }
            }

            access.WriteLogFile(username, password);
            return 0;
        }

        public void Logout()
        {
            Environment.Exit(0);
        }

        public void DeleteUser(User user)
        {

            using (var db = new App_Context())
            {
                var result = db.Users.Find(user.Id);
                IList<MessageDTO> messageDTOs = db.Messages.Where(m => m.ReceiverName == result.Username || m.SenderName == result.Username).ToList();

                string[] file;
                if (messageDTOs.Count > 0)
                {
                    db.Messages.RemoveRange(messageDTOs);
                    db.SaveChanges();
                }

                bool isTheAdmin = IsAdmin(user.Id);

                if (result!=null && !isTheAdmin)
                {
                   Console.ForegroundColor = ConsoleColor.DarkRed;
                   Console.WriteLine("The User will delete permanently.Do you want to continue? Y/N");
                   Console.ResetColor();
                   string x = Console.ReadLine();
                   if (x == "Y" || x == "y")
                   {

                       file = new string[] { $"====  User Deleted ====\r\nAt:{DateTime.Now} with Username:{result.Username}" };
                       File.AppendAllLines(adminFile, file);
                       db.Users.Remove(result);
                       db.SaveChanges();
                       Console.ForegroundColor = ConsoleColor.DarkGreen;
                       Console.WriteLine("The user deleted succesfully.");
                       Console.ResetColor();
                   }
                   else
                   {
                       Console.ForegroundColor = ConsoleColor.DarkGray;
                       Console.WriteLine("Deletion of the user has been canceled");
                       Console.ResetColor();
                   }
                }
                else if (result != null && isTheAdmin)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid Action.You try to delete the Administrator.");
                    Console.ResetColor();
                }

            }
        }

        public void ViewUser()
        {
            using (var db = new App_Context())
            {
                var result = db.Users.ToList();

                foreach (var item in result)
                {
                    Console.WriteLine($"User Id: {item.Id}"+$"\nUsername:{item.Username}"+$"\nAccess level:{item.AccessId}"+$"\nEmail:{item.Email}\n");
                }
            }

        }

        static bool IsAdmin(int id)
        {
            using (var db = new App_Context())
            {
                var result = db.Users.Where(i => i.Id == id).Select(p => p.Username);

                foreach (var item in result)
                {
                    if (item.Equals("admin"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void UpdateUser(int id,string pass)
        {
            FileAccess access = new FileAccess(adminFile);
            using (var db = new App_Context())
            {
                var result = db.Users.Find(id);

                bool isTheAdmin = IsAdmin(id);

                if (result != null && !isTheAdmin)
                {
                    var salt = Password.GetSalt();
                    var hash = Password.Hash(pass, salt);
                    result.Password = Convert.ToBase64String(hash);
                    result.Salt = Convert.ToBase64String(salt);
                    access.WriteLogFile(id);
                    db.SaveChanges();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Update succesfully!");
                    Console.ResetColor();
                }
                else if (result != null && isTheAdmin)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid Action.You try to change the Admin privilages.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("The Id doesn't match.Update failed.");
                    Console.ResetColor();
                }
            }
        }

        public void UpdateUser(int id,AccessId accessid)
        {
            FileAccess access = new FileAccess(adminFile);
            using (var db = new App_Context())
            {
                var result = db.Users.Find(id);
                bool isTheAdmin = IsAdmin(id);
                if (result!=null && !isTheAdmin)
                {
                    result.AccessId = accessid;
                    access.WriteLogFile(id);
                    db.SaveChanges();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Update succesfully!");
                    Console.ResetColor();
                }
                else if(result!=null && isTheAdmin)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid Action.You try to change the Admin privilages.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("The Id doesn't match.Update failed.");
                    Console.ResetColor();
                }
            }
        }

        public void UpdateEmail(int id, string email)
        {
            FileAccess access = new FileAccess(adminFile);

            using (var db = new App_Context())
            {
                var result = db.Users.Find(id);

                if(result!=null)
                {
                    result.Email = email;
                    access.WriteLogFile(id);
                    db.SaveChanges();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Update succesfully!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("The Id doesn't match.Update failed.");
                    Console.ResetColor();
                }
            }
        }
    }
}