using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Verification.Model;
using Verification.Models;

namespace Verification.Services
{
    public interface IUserOperation
    {
        //get users
        IEnumerable<User> GetUser();

        //add user and token to data base
        void AddUser(User value);

        //verify user

        Task<User> Verify(string value);

    }


    public class UserOperation : IUserOperation
    {
        private readonly VerificationContext _context;

        public UserOperation(VerificationContext context)
        {
            _context = context;
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public string SendMail(string value)
        {
            //generate token 
            string token = RandomString(6);

            //instantiate mimemsg
            var message = new MimeMessage();

            //from add
            message.From.Add(new MailboxAddress("TL;DM", "talklessDM@gmail.com"));
            //to add
            message.To.Add(new MailboxAddress("Hi", value));
            //subject
            message.Subject = "Verification Mail";

            //body
            message.Body = new TextPart("plain")
            {
                Text = "Welcome to TL;DM your temporaray token is  " + token + " Welcome Aboard!"
            };

            //Configure and send email

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("talklessDM@gmail.com", "tldm1234");
                client.Send(message);
                client.Disconnect(true);

            }
            return token;
        }

        public void AddUser(User value)
        {
            
            string token = SendMail(value.Email);
            User user = new User() { Email = value.Email, Workspace = value.Workspace , Token = token };
            _context.User.Add(user);
             _context.SaveChanges();
        }

        public async Task<User> Verify(string value)
        {
            var user =  await _context.User.FirstOrDefaultAsync(x => x.Token == value);
            if (user != null)
            {
                _context.User.Remove(user);
                _context.SaveChanges();
            }
            return user;
        }

        public IEnumerable<User> GetUser()
        {
            var y = _context.User;
            return y;
        }

    }
}
