using FluentValidation;
using StudentManagement.Application.Commands;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Validator
{
    public class UpdateGradeValidator : AbstractValidator<UpdateGradeCommand>
    {
        public UpdateGradeValidator()
        {
            RuleFor(g => g.Grade.GradeID)
                .NotEmpty().WithMessage("GradeID is required")
                .GreaterThan(0).WithMessage("GradeID must be greater than 0");
            RuleFor(g => g.Grade.StudentID)
                .NotEmpty().WithMessage("StudentID is required")
                .GreaterThan(0).WithMessage("StudentID must be greater than 0");
            RuleFor(g => g.Grade.CourseID)
                .NotEmpty().WithMessage("CourseID is required")
                .GreaterThan(0).WithMessage("CourseID must be greater than 0");
            RuleFor(g => g.Grade.Grade)
                .NotEmpty().WithMessage("Grade is required")
                .Must(Grade => Grade == 'A' || Grade == 'B' || Grade == 'C' || Grade == 'D' || Grade == 'F')
                .WithMessage("Grade must be one of the following values: A, B, C, D, or F");
        }
    }
}
