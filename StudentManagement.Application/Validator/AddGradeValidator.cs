using FluentValidation;
using StudentManagement.Application.Commands;

namespace StudentManagement.Application.Validator
{
    public class AddGradeValidator : AbstractValidator<AddGradeCommand>
    {
        public AddGradeValidator() 
        {
            RuleFor(g => g.gradeEntity.StudentID)
                .NotEmpty().WithMessage("StudentID is required")
                .GreaterThan(0).WithMessage("StudentID must be greater than 0");
            RuleFor(g => g.gradeEntity.CourseID)
                .NotEmpty().WithMessage("CourseID is required")
                .GreaterThan(0).WithMessage("CourseID must be greater than 0");
            RuleFor(g => g.gradeEntity.Grade)
                .NotEmpty().WithMessage("Grade is required")
                .Must(Grade => Grade == 'A' || Grade == 'B' || Grade == 'C' || Grade == 'D' || Grade == 'F')
                .WithMessage("Grade must be one of the following values: A, B, C, D, or F");
        }
    }
}
