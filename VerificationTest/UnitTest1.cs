using Xunit;
using System;
using System.Linq;
using System.Collections.Generic;
using Verification.Controllers;
using Microsoft.EntityFrameworkCore;
using Verification.Models;
using Microsoft.AspNetCore.Mvc;
using Verification.Services;
using Verification.Model;

namespace VerificationTest
{
    public class VerifyTest
    {
        UserOperation _operation;
        UsersController _controller;
        public VerifyTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<VerificationContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var Usercontext = new VerificationContext(optionsBuilder.Options);
            _operation = new UserOperation(Usercontext);
            _controller = new UsersController(_operation);
            CreateTestData(Usercontext);
        }
        public void CreateTestData(VerificationContext Usercontext)
        {
            List<User> user = new List<User>() { new User { Email = "preethamdbangera@gmail.com", Token = "123qwe" },
                                                 new User { Email = "n.varunnaidu@gmail.com", Token = "456rty" }
                                               };
            Usercontext.AddRange(user);
            Usercontext.SaveChanges();


        }
       
        [Fact]
        public void TestPostUser()
        {
            string value = "anandkumar316@gmail.com";
            var result = _controller.PostUser(value);
           // Console.Write(result.ToString);
            var resultAsOkObjectResult = result as OkObjectResult;
            var user = resultAsOkObjectResult.Value;
           Assert.Equal(user, value);
        }

        [Fact]
        public async void TestPostVerify()
        {
            string value = "123qwe";
            var result = await _controller.PostVerify(value);
            var resultAsOkObjectResult = result as OkObjectResult;
            var user = resultAsOkObjectResult.Value as User;
            Assert.Equal(user.Token, value);
        }
    }
}
