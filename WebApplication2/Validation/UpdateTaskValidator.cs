namespace WebApplication2.Validation
{
    using FluentValidation;
    using WebApplication2.Application;

    public class UpdateTaskValidator : AbstractValidator<UpdateTasksDto>
    {
        public UpdateTaskValidator()
        {
            RuleFor(t => t.Name).NotEmpty().WithMessage("Нвзвание не должно быть пустым").MaximumLength(50).WithMessage("Слишком длинное название");
            RuleFor(t => t.Description).NotEmpty().WithMessage("Описание не должно быть пустым").MaximumLength(1000).WithMessage("Слишком длинное описание");
            RuleFor(t => t.Status).IsEnumName(typeof(Domain.ItemTaskStatus), caseSensitive: false).WithMessage("Статус должен быть одним из следующих: Pending, InProgress, Done");
            RuleFor(t => t.Deadline).GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("Выберите будущую дату");
        }
    }
}
