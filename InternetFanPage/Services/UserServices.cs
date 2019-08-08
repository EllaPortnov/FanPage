using InternetFanPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace InternetFanPage.Services
{
    public class UserServices
    {

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

        public IList<ProductsUser> ProductsByUser()
        {
            using (var context = new FanPageContext())
            {
                var Query = from s in context.Sales
                    join u in context.Users
                        on s.UserID equals u.UserID
                    join p in context.Products
                        on s.ProductID equals p.ProductID
                    group u by u.UserID into newUser
                    select new ProductsUser()
                {
                    UserExpense = newUser.Key,
                    UserName = newUser.Sum(p=>p.IsAdmin)
                };
                IList<ProductsUser> finalResult = Query.ToList();
                return finalResult;
//                return context.Users.Join(context.Sales, u => u.UserID, s => s.UserID, (user, sale) => new
//                    {
//                        user.Username,
//                        sale
//                    })
//                    .GroupBy(x => x.sale.UserID)
//                    .Select(x => new ProductsUser()
//                    {
//                        UserName = x.Key,
//                        UserExpense = x.Count()
//                    })
//                    .ToArray();
            }
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