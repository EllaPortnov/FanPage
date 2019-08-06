using InternetFanPage.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


namespace InternetFanPage.Services
{
    public class UserServices
    {
        public LoginResult FacebookLogin(string userId, string accessToken)
        {
            using (var context = new FanPageContext())
            {
                var userDetails = context.Users.Where(p => p.FBUserId == userId).FirstOrDefault();

                if (userDetails == null)
                {
                    // TODO: request facebook for the details and create user
                }

                var token = convertToMd5(DateTime.Now.ToString());

                return new LoginResult()
                {
                    LoginSucceeded = true,
                    Token = token
                };
            }
        }

        public LoginResult AttemptLogin(LoginDetails input)
        {
            if (input == null)
                return LoginResult.Failed;

            using (var context = new FanPageContext())
            {
                var userDetails = context.Users.Where(p => p.Username == input.Username).FirstOrDefault();

                if (userDetails == null)
                    return LoginResult.Failed;

                var encryptedPassword = convertToMd5(input.Password);
                var token = convertToMd5(DateTime.Now.ToString());

                if (!encryptedPassword.Equals(userDetails.Password))
                    return LoginResult.Failed;

                return new LoginResult()
                {
                    LoginSucceeded = true,
                    Token = token
                };
            }
        }



        public bool Register(RegisterDetails UserInput)
        {
            if (UserInput is null)
                return false;
            using (var Context = new FanPageContext())
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
            using (var context = new FanPageContext())
            {
                return context.Users.FirstOrDefault(u => u.Username == userName);
            }
        }

        public bool DoesUserExists(string username)
        {
            using (var context = new FanPageContext())
            {
                return context.Users.Count(u => u.Username == username) > 0;
            }
        }
    }
}