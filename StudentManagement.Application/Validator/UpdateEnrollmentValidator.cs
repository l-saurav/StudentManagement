using FluentValidation;
using StudentManagement.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Validator
{
    public class UpdateEnrollmentValidator : AbstractValidator<UpdateEnrollmentCommand>
    {
        public UpdateEnrollmentValidator()
        {
            RuleFor(e => e.enrollment.EnrollmentID)
                .NotEmpty().WithMessage("EnrollmentID is required")
                .GreaterThan(0).WithMessage("EnrollmentID must be greater than 0");
            RuleFor(e => e.enrollment.StudentID)
                .NotEmpty().WithMessage("StudentID is required") // Ensures StudentID is not empty
                .GreaterThan(0).WithMessage("StudentID must be greater than 0"); // Ensures StudentID is greater than 0
            RuleFor(e => e.enrollment.CourseID)
                .NotEmpty().WithMessage("CourseID is required") // Ensures CourseID is not empty
                .GreaterThan(0).WithMessage("CourseID must be greater than 0"); // Ensures CourseID is greater than 0
            RuleFor(e => e.enrollment.EnrollmentDate)
                .NotNull().WithMessage("Enrollment Date is required") // Ensures EnrollmentDate is not null
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Enrollment Date must be less than or equal to the current date"); // Ensures EnrollmentDate is in the past or present
        }
    }
}
