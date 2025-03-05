using FluentValidation;
using StudentManagement.Application.Commands;

namespace StudentManagement.Application.Validator
{
    public class AddCourseValidator : AbstractValidator<AddCourseCommand>
    {
        public AddCourseValidator()
        {
            RuleFor(c => c.Course.CourseName)
                .NotEmpty().WithMessage("Course Name is required")
                .MinimumLength(3).WithMessage("Course Name must not be less than 3 characters")
                .MaximumLength(30).WithMessage("Course Name must not exceed 30 characters");
            RuleFor(c => c.Course.CourseCode)
                .NotEmpty().WithMessage("Course Code is required")
                .MinimumLength(3).WithMessage("Course Code must not be less than 3 characters")
                .MaximumLength(10).WithMessage("Course Code must not exceed 10 characters");
            RuleFor(e => e.Course.CreditHours)
                .NotEmpty().WithMessage("Credit Hours is required")  // Ensure CreditHours is not empty
                .GreaterThanOrEqualTo(1).WithMessage("Credit Hours must be at least 1") // Ensure CreditHours is >= 1
                .LessThanOrEqualTo(5).WithMessage("Credit Hours must not exceed 5"); // Ensure CreditHours is <= 5
        }
    }
}
