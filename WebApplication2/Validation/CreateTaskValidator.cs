namespace WebApplication2.Validation
{
    using FluentValidation;
    using WebApplication2.Application;
    using WebApplication2.Domain;

    public class CreateTaskValidator : AbstractValidator<CreateTasksDto>
    {
        public CreateTaskValidator()
        {
            RuleFor(t => t.Name).NotEmpty().MaximumLength(50);
            RuleFor(t => t.Description).NotEmpty().MaximumLength(1000);
            RuleFor(t => t.Deadline).GreaterThanOrEqualTo(DateTime.UtcNow);
        }
    }
}
