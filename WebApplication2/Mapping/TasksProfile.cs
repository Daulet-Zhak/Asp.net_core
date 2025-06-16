using AutoMapper;
using WebApplication2.Application;
using WebApplication2.Domain;

namespace WebApplication2.Mapping
{
    public class TasksProfile : Profile
    {
        public TasksProfile()
        {
            CreateMap<CreateTasksDto, ItemTask>();
            CreateMap<UpdateTasksDto, ItemTask>();
            CreateMap<ItemTask, CreateTasksDto>();
        }
    }
}
