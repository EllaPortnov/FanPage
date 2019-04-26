using InternetFanPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace InternetFanPage.Services
{
    public class UserServices
    {
        public bool Register(RegisterDetails UserInput)
        {
            if (UserInput is null)
                return false;
            using (var Context = new PageContext())
            {
                User newUser = new User()
                {
                    Address = UserInput.Address,
                    FirstName = UserInput.FirstName,
                    IsAdmin = 0,
                    LastName = UserInput.LastName,
                    Password = convertToMd5(UserInput.Password),
                    Phone = UserInput.Phone,
                    Username = UserInput.Username
                };

                Context.Users.Add(newUser);
                Context.SaveChanges();
            }
            return true;
        }

        private string convertToMd5(string target)
        {
            var cipher = new MD5CryptoServiceProvider();
            var passwordBytes = Encoding.UTF8.GetBytes(target);

            passwordBytes = cipher.ComputeHash(passwordBytes);

            var sb = new StringBuilder();

            for (int i = 0; i < passwordBytes.Length; i++)
            {
                sb.Append(passwordBytes[i].ToString("x2").ToLower());
            }

            return sb.ToString();
        }

        public User GetUser(string userName)
        {
            using (var context = new PageContext())
            {
                return context.Users.FirstOrDefault(u => u.Username == userName);
            }
        }

        public bool DoesUserExists(string username)
        {
            using (var context = new PageContext())
            {
                return context.Users.Count(u => u.Username == username) > 0;
            }
        }
    }
}