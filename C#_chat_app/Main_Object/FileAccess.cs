using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Main_Object
{
    public class FileAccess
    {
        protected string _path;

        public FileAccess(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException();
            _path = path;
        }

        static bool ReceiverExist(string receiver)
        {
            using (App_Context db = new App_Context())
            {
                var exist = db.Users.Where(i => i.Username == receiver).FirstOrDefault();

                if (exist!=null)
                {
                    return true;
                }
            }
            return false;
        }

        public void WriteMessage(string sender,string receiver,string message)
        {
            if (ReceiverExist(receiver))
            {
                string[] lines = new string[] {$"======== {DateTime.Now} ========\r\nsender: {sender}, " +
                $"receiver: {receiver}\r\n{message}"};
                File.AppendAllLines(_path, lines);
            }            
        }

        //Login log
        public void WriteLogFile(string username, string password)
        {
            string[] file = new string[] { $"==== User Login ====\r\nAt:{DateTime.Now} with Username:{username}" };
            File.AppendAllLines(_path, file);
        }

        //Create Account log
        public void WriteLogFile(string username, string password, string email)
        {
            string[] file = new string[] { $"==== Account Created ====\r\nAt:{DateTime.Now} with Username:{username}" };
            File.AppendAllLines(_path, file);
        }

        //Update user log
        public void WriteLogFile(int id)
        {
            using (var db = new App_Context())
            {
                var user = db.Users.Where(p=>p.Id==id).ToList();
                foreach (var item in user)
                {
                    string[] file = new string[] { $"==== User Updated ====\r\nAt:{DateTime.Now} with Username:{item.Username}" };
                    File.AppendAllLines(_path, file);
                }
            }
        }
    }
}
