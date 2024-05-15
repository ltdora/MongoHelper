using HelloWorldMongoDB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelloWorldMongoDB.Services.Interfaces
{
    public interface IUsersServices
    {
        Task<List<Users>> GetUsersAsync();
        Task<Users> GetUserAsyncById(string id);
        Task CreateUserAsync(Users user);
        Task UpdateUserAsync(string id, Users user);
        Task DeleteUserAsync(string id);
    }
}
