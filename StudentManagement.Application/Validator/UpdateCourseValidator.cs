using FluentValidation;
using StudentManagement.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Validator
{
    public class UpdateCourseValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseValidator()
        {
            RuleFor(c => c.course.CourseID)
                .NotEmpty().WithMessage("CourseID is required")
                .GreaterThan(0).WithMessage("CourseID must be greater than 0");
            RuleFor(c => c.course.CourseName)
                .NotEmpty().WithMessage("Course Name is required")
                .MinimumLength(3).WithMessage("Course Name must not be less than 3 characters")
                .MaximumLength(30).WithMessage("Course Name must not exceed 30 characters");
            RuleFor(c => c.course.CourseCode)
                .NotEmpty().WithMessage("Course Code is required")
                .MinimumLength(3).WithMessage("Course Code must not be less than 3 characters")
                .MaximumLength(10).WithMessage("Course Code must not exceed 10 characters");
            RuleFor(e => e.course.CreditHours)
                .NotEmpty().WithMessage("Credit Hours is required")  // Ensure CreditHours is not empty
                .GreaterThanOrEqualTo(1).WithMessage("Credit Hours must be at least 1") // Ensure CreditHours is >= 1
                .LessThanOrEqualTo(5).WithMessage("Credit Hours must not exceed 5"); // Ensure CreditHours is <= 5
        }
    }
}
