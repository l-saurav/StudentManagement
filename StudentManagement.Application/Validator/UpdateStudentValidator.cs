using FluentValidation;
using StudentManagement.Application.Commands;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Validator
{
    public class UpdateStudentValidator : AbstractValidator<UpdateStudentCommand>
    {
        public UpdateStudentValidator(IStudentRepository studentRepository) 
        {
            RuleFor(s => s.student.StudentID)
                .NotEmpty().WithMessage("StudentID is required")
                .GreaterThan(0).WithMessage("StudentID must be greater than 0");

            RuleFor(s => s.student.FullName)
                .NotEmpty().WithMessage("FullName is required")
                .MinimumLength(3).WithMessage("Full Name must not be less than 3 characters")
                .MaximumLength(50).WithMessage("Full Name must not exceed 50 characters");

            RuleFor(s => s.student.DateOfBirth)
                .NotEmpty().WithMessage("Date of Birth is required")
                .LessThan(DateTime.Now).WithMessage("Date of Birth must be less than current date");

            RuleFor(s => s.student.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid")
                .MaximumLength(50).WithMessage("Email must not exceed 50 characters")
                .MustAsync(async (email, cancellationToken) =>
                {
                    var isUnique = await studentRepository.isEmailUniqueAsync(email);
                    return isUnique;
                }).WithMessage("Email already exists")
                .OverridePropertyName("Student.Email"); // Ensure correct error field name

            RuleFor(s => s.student.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number is required")
                .Matches(@"^\d{10}$").WithMessage("Phone Number must be 10 digits")
                .MaximumLength(10).WithMessage("Phone Number must not exceed 10 characters");

            RuleFor(s => s.student.RegistrationDate)
                .NotEmpty().WithMessage("Registration Date is required")
                .LessThan(DateTime.Now).WithMessage("Registration Date must be less than current date");
        }
    }
}
