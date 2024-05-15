using HelloWorldMongoDB.Models;
using HelloWorldMongoDB.Services.Implements;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelloWorldMongoDB.Controllers
{
    [Controller]
    [Route("api / [controller]")]
    public class UserController : Controller
    {
        private readonly UsersServices _services;

        public UserController(UsersServices services)
        {
            _services = services;
        }

        [HttpGet("get-all")]
        public async Task<List<Users>> GetAllUsers()
        {
            return await _services.GetUsersAsync();
        }

        [HttpGet("get-by-id")]
        public async Task<Users> GetUserById(string id)
        {
            var users = await _services.GetUserAsyncById(id);
            if (users == null) 
            {
                return null;
            }
            return users;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(Users user)
        {
            await _services.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetAllUsers), new { id = user.Id }, user);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(string id, Users user)
        {
            var userUpdate = await _services.GetUserAsyncById(id);
            if (userUpdate == null)
            {
                return NotFound();
            }
            user.Id = userUpdate.Id;
            await _services.UpdateUserAsync(id, user);
            return NoContent();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var userUpdate = await _services.GetUserAsyncById(id);
            if (userUpdate == null)
            {
                return NotFound();
            }
            await _services.DeleteUserAsync(id);
            return NoContent();
        }

        //[HttpGet]
        //public async Task<List<Users>> Get()
        //{
        //    return await _services.GetAsync();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] Users users)
        //{
        //    await _services.CreateAsync(users);
        //    return CreatedAtAction(nameof(Get), new {id = users.Id}, users);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> AddPost(string id, [FromBody] string postId)
        //{
        //    await _services.AddPostAsync(id, postId);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    await _services.DeleteAsync(id);
        //    return NoContent();
        //}
    }
}
