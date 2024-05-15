using HelloWorldMongoDB.Helpers;
using HelloWorldMongoDB.Models;
using HelloWorldMongoDB.Services.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelloWorldMongoDB.Services.Implements
{
    public class UsersServices : IUsersServices
    {
        private readonly IMongoCollection<Users> _usersCollection;
        MongoHelper<Users> mongoHelper;

        public UsersServices(IOptions<MongoDBSettings> setting)
        {
            MongoClient client = new MongoClient(setting.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(setting.Value.DatabaseName);
            _usersCollection = database.GetCollection<Users>("Users");

            mongoHelper = new MongoHelper<Users>(setting.Value.ConnectionString, setting.Value.DatabaseName, "Users");

        }

        public async Task<List<Users>> GetUsersAsync()
        {
            var lstUsers = await mongoHelper.GetAllAsync();

            //var lstUsers = await _usersCollection.Find(_ => true).ToListAsync();
            return lstUsers;
        }

        public async Task<Users> GetUserAsyncById(string id)
        {
            var user = await mongoHelper.GetByIdAsync(id);

            //var user = await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return user;
        }

        public async Task CreateUserAsync(Users user)
            //=> await _usersCollection.InsertOneAsync(user);
            => await mongoHelper.CreateAsync(user);

        public async Task UpdateUserAsync(string id, Users user)
            //=> await _usersCollection.ReplaceOneAsync(x => x.Id == id, user);
            => await mongoHelper.UpdateAsync(id, user);

        public async Task DeleteUserAsync(string id)
            //=> _usersCollection.DeleteOneAsync(x => x.Id == id);
            => await mongoHelper.DeleteAsync(id);

    }
}
