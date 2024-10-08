﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using static UserApi.Models.Dto;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            using (var context = new UserDbContext())
            {
                return StatusCode(201, context.newuser.ToList());
            }
        }

        [HttpPost]
        public ActionResult<User> Post(CreateUserDto CreateUserDto)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = CreateUserDto.Name,
                Age = CreateUserDto.Age,
                License = CreateUserDto.License
            };

            using (var context = new UserDbContext())
            {
                context.newuser.Add(user);
                context.SaveChanges();
                return StatusCode(201, user);
            }
        }

        [HttpPut("{azon}")]
        public ActionResult<User> Put(Guid azon, UpdateUserDto updateUserDto)
        {
            using (var context = new UserDbContext())
            {
                var existingUser = context.newuser.FirstOrDefault(x => x.Id == azon);

                existingUser.Name = updateUserDto.Name;
                existingUser.Age = updateUserDto.Age;
                existingUser.License = updateUserDto.License;

                context.newuser.Update(existingUser);
                context.SaveChanges();

                return StatusCode(200, existingUser);
            }
        }
    }
}
