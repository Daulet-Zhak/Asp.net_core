using MongoDB.Bson.Serialization.Attributes;


namespace WebApplication2.Application
{
    public class CreateTasksDto
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
    }
    public class UpdateTasksDto
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime Deadline { get; set; }
    }
}
