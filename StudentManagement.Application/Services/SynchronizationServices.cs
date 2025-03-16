using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Services
{
    public class SynchronizationServices : ISynchronizationService
    {

        private readonly IStudentRepository _primaryStudentRepo;
        private readonly IStudentRepositoryBackup _backupStudentRepo;
        private readonly ICourseRepository _primaryCourseRepo;
        private readonly ICourseRepositoryBackup _backupCourseRepo;

        public SynchronizationServices(IStudentRepository primaryStudentRepo, IStudentRepositoryBackup backupStudentRepo,
            ICourseRepository primaryCourseRepo, ICourseRepositoryBackup backupCourseRepo)
        {
            _primaryStudentRepo = primaryStudentRepo;
            _backupStudentRepo = backupStudentRepo;
            _primaryCourseRepo = primaryCourseRepo;
            _backupCourseRepo = backupCourseRepo;
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
            Console.WriteLine("Synchronization on Student table completed.");
        }

        public async Task SynchronizeCoursesAsync()
        {
            //Fetch Courses from Primary and Backup Database
            var primaryCourse = await _primaryCourseRepo.GetAllAsync();
            Console.WriteLine($"There are {primaryCourse.Count()} Courses in primary database");
            var backupCourse = await _backupCourseRepo.GetAllAsync();
            Console.WriteLine($"There are {backupCourse.Count()} Courses in backup database");

            //Converting Backup Database to Dictionary for quicker lookup
            var backupCourseDict = backupCourse.ToDictionary(c => c.CourseID);

            foreach (var course in primaryCourse)
            {
                if(!backupCourseDict.TryGetValue(course.CourseID, out var existingCourse))
                {
                    //Adding New course to the backup database if it does not exist in the primary database
                    var newCourse = new CourseEntity
                    {
                        CourseName = course.CourseName,
                        CourseCode = course.CourseCode,
                        CreditHours = course.CreditHours
                    };
                    Console.WriteLine($"Inserting Course {course.CourseID} into the backup database");
                    await _backupCourseRepo.AddAsync(newCourse);
                }
                else
                {
                    //Check if any field have changed
                    if(existingCourse.CourseName != course.CourseName ||
                        existingCourse.CourseCode != course.CourseCode || 
                        existingCourse.CreditHours != course.CreditHours)
                    {
                        //Update the Course record if something have changed
                        existingCourse.CourseName = course.CourseName;
                        existingCourse.CourseCode = course.CourseCode;
                        existingCourse.CreditHours = course.CreditHours;
                        Console.WriteLine($"Updating the record of Course {course.CourseID} ");
                        await _backupCourseRepo.UpdateAsync(existingCourse.CourseID, existingCourse);
                    }
                }
            }

            //Deleting Course if not present in primary database
            var primaryCourseIDs = primaryCourse.Select(c => c.CourseID).ToHashSet();
            var courseToDelete = backupCourse.Where(c => !primaryCourseIDs.Contains(c.CourseID)).ToList();
            foreach (var course in courseToDelete)
            {
                Console.WriteLine($"Deleting Course {course.CourseID} from the backup Database");
                await _backupCourseRepo.DeleteAsync(course.CourseID);
            }
            Console.WriteLine("Synchronization on Course table completed!");
        }
    }
}
