using AutoMapper.Configuration;
using MongoDB.Driver;
using WebApplication2.Domain;
using WebApplication2.Services;

namespace WebApplication2.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IMongoCollection<ItemTask> _tasks;
        public TaskRepository(MongoDbService db)
        {
            _tasks = db.Database.GetCollection<ItemTask>("Itemtasks");
        }
        public async Task<List<ItemTask>> GetTasks(string user_id)
        {
            return await _tasks.Find(t => t.UserId == user_id).ToListAsync();
        }
        public async Task<ItemTask> GetTaskById(string task_id, string user_id)
        {
            return await _tasks.Find(t => t.Id == task_id && t.UserId == user_id).FirstOrDefaultAsync();
        }
        public async Task CreateTask(ItemTask task)
        {
            await _tasks.InsertOneAsync(task);
        }
        public async Task UpdateTask(ItemTask task, string task_id) =>

            await _tasks.ReplaceOneAsync(t => t.Id == task_id && t.UserId == task.UserId, task);
        
        public async Task DeleteTask(string user_id, string task_id)
        {
            await _tasks.DeleteOneAsync(t => t.Id == task_id && t.UserId == user_id);
        }
    }
}
