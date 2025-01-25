using FluentValidation;
using Task_Manager.ViewModels;

namespace Task_Manager.Configurations
{
    public class DutyCreateModelValidator : AbstractValidator<DutyCreateModel>
    {
        public DutyCreateModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.StatusId).Must(id => new[] { 1, 2, 3 }.Contains(id)).WithMessage("Status must be chosen");
            RuleFor(x => x.DueDate).GreaterThanOrEqualTo(DateTime.Today).WithMessage("Due Date cannot be earlier than today");
        }
    }
}
