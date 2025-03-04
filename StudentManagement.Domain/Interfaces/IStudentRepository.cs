﻿using StudentManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Domain.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<StudentEntity>> GetStudents();
        Task<StudentEntity> GetStudentByIdAsync(int StudentID);
        Task<StudentEntity> AddStudentAsync(StudentEntity student);
        Task<StudentEntity> UpdateStudentAsync(int StudentID, StudentEntity student);
        Task<bool> DeleteStudentAsync(int StudentID);

        //For FluentValidation
        Task<bool> isEmailUniqueAsync(string email);
    }
}
