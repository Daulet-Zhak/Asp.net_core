using WebApplication2.Domain;

namespace WebApplication2.Repositories
{
    public interface ITaskRepository
    {
        Task<List<ItemTask>> GetTasks(string user_id);
        Task<ItemTask> GetTaskById(string task_id, string user_id);
        Task CreateTask(ItemTask task);
        Task UpdateTask(ItemTask task, string task_id);
        Task DeleteTask(string user_id, string task_id);
    }
}
