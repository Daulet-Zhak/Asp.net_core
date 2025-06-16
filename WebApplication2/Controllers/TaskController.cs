using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Security.Claims;
using WebApplication2.Application;
using WebApplication2.Domain;
using WebApplication2.Repositories;
using WebApplication2.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers
{
    [Route("tasks/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMongoCollection<ItemTask> _tasks;
        private readonly IMongoCollection<User> _users;
        private readonly IMapper _mapper;
        private readonly ITaskRepository _repo;

        public TaskController(MongoDbService db, IMapper mapper, ITaskRepository repo)
        {
            _tasks = db.Database.GetCollection<ItemTask>("tasks");
            _users = db.Database.GetCollection<User>("users");
            _mapper = mapper;
            _repo = repo;
        }

        // GET: api/<TaskController>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> tasks()
        {
            var user_id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var tasks = await _repo.GetTasks(user_id);
            if(tasks == null)
            {
                return NoContent();
            }
            return Ok(tasks);
        }

        // GET api/<TaskController>/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var user_id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var task = await _repo.GetTaskById(id, user_id);
            if(task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        // POST api/<TaskController>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> tasks(CreateTasksDto dto)
        {
            var user_id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _users.Find(u => u.Id == user_id).FirstOrDefaultAsync();

            ItemTask newtask = _mapper.Map<ItemTask>(dto);
            newtask.Status = Domain.ItemTaskStatus.Pending;
            newtask.UserId = user_id;
            newtask.Username = user.Username;

            await _repo.CreateTask(newtask);

            return Ok(newtask);
        }

        // PUT api/<TaskController>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> tasks( string id, UpdateTasksDto dto)
        {
            var user_id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var task = await _repo.GetTaskById(id, user_id);
            if (task == null)
            {
                return BadRequest();
            }
            _mapper.Map(dto, task);
            await _repo.UpdateTask(task, id);
            return Ok();
        }

        // DELETE api/<TaskController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> tasks(string id)
        {
            var user_id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _repo.DeleteTask(user_id, id);
            return Ok();
        }
    }
}
