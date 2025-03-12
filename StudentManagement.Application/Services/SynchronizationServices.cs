using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Services
{
    public class SynchronizationServices : ISynchronizationService
    {

        private readonly IStudentRepository _primaryStudentRepo;
        private readonly IStudentRepositoryBackup _backupStudentRepo;

        public SynchronizationServices(IStudentRepository primaryStudentRepo, IStudentRepositoryBackup backupStudentRepo)
        {
            _primaryStudentRepo = primaryStudentRepo;
            _backupStudentRepo = backupStudentRepo;
        }

        public async Task SynchronizeStudentsAsync()
        {
            // Fetch students from primary and backup database
            Console.WriteLine("Fetching primary data information from main database");
            var primaryStudents = await _primaryStudentRepo.GetAllAsync();  // Ensure it is awaited
            Console.WriteLine($"Primary database has {primaryStudents.Count()} students");
            Console.WriteLine("Fetching backup data information from backup database");
            var backupStudents = await _backupStudentRepo.GetAllAsync();   // Ensure it is awaited
            Console.WriteLine($"Secondary database has {backupStudents.Count()} students");

            // Convert backup students into a dictionary for quick lookup
            var backupStudentDict = backupStudents.ToDictionary(s => s.StudentID);

            // Handle inserts and updates
            foreach (var student in primaryStudents)
            {
                if (!backupStudentDict.TryGetValue(student.StudentID, out var existingStudent))
                {
                    // Insert new student in backup database
                    var newStudent = new StudentEntity
                    {
                        FullName = student.FullName,
                        DateOfBirth = student.DateOfBirth,
                        Email = student.Email,
                        PhoneNumber = student.PhoneNumber,
                        RegistrationDate = student.RegistrationDate
                    };
                    Console.WriteLine($"Inserting new student {student.StudentID} into backup database.");
                    await _backupStudentRepo.AddAsync(newStudent);
                }
                else
                {
                    // Check if any field has changed
                    if (existingStudent.FullName != student.FullName ||
                        existingStudent.DateOfBirth != student.DateOfBirth ||
                        existingStudent.Email != student.Email ||
                        existingStudent.PhoneNumber != student.PhoneNumber)
                    {
                        existingStudent.FullName = student.FullName;
                        existingStudent.DateOfBirth = student.DateOfBirth;
                        existingStudent.Email = student.Email;
                        existingStudent.PhoneNumber = student.PhoneNumber;
                        Console.WriteLine($"Updating student {student.StudentID} in backup database.");
                        await _backupStudentRepo.UpdateAsync(existingStudent.StudentID, existingStudent);
                    }
                }
            }

            // Handle deletion: Remove students in backup DB that do not exist in primary
            var primaryStudentIDs = primaryStudents.Select(s => s.StudentID).ToHashSet();
            var studentsToDelete = backupStudents.Where(s => !primaryStudentIDs.Contains(s.StudentID)).ToList();

            foreach (var student in studentsToDelete)
            {
                Console.WriteLine($"Deleting student {student.StudentID} from backup database.");
                await _backupStudentRepo.DeleteAsync(student.StudentID);
            }
            Console.WriteLine("Synchronization completed.");
        }
    }
}
