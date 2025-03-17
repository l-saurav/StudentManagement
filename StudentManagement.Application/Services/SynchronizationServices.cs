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
        private readonly IEnrollmentRepository _primaryEnrollmentRepo;
        private readonly IEnrollmentRepositoryBackup _backupEnrollmentRepo;
        private readonly IGradeRepository _primaryGradeRepo;
        private readonly IGradeRepositoryBackup _backupGradeRepo;

        public SynchronizationServices(IStudentRepository primaryStudentRepo, IStudentRepositoryBackup backupStudentRepo,
            ICourseRepository primaryCourseRepo, ICourseRepositoryBackup backupCourseRepo,
            IEnrollmentRepository primaryEnrollmentRepo, IEnrollmentRepositoryBackup backupEnrollmentRepo,
            IGradeRepository primaryGradeRepo, IGradeRepositoryBackup backupGradeRepo)
        {
            _primaryStudentRepo = primaryStudentRepo;
            _backupStudentRepo = backupStudentRepo;
            _primaryCourseRepo = primaryCourseRepo;
            _backupCourseRepo = backupCourseRepo;
            _primaryEnrollmentRepo = primaryEnrollmentRepo;
            _backupEnrollmentRepo = backupEnrollmentRepo;
            _primaryGradeRepo = primaryGradeRepo;
            _backupGradeRepo = backupGradeRepo;
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
                        StudentID = student.StudentID,
                        FullName = student.FullName,
                        DateOfBirth = student.DateOfBirth,
                        Email = student.Email,
                        PhoneNumber = student.PhoneNumber,
                        RegistrationDate = student.RegistrationDate
                    };
                    Console.WriteLine($"Inserting new student {newStudent.StudentID} into backup database.");
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
                        existingStudent.RegistrationDate = student.RegistrationDate;
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
                        CourseID = course.CourseID,
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

        public async Task SynchronizeEnrollmentAsync()
        {
            //Fetch record from both database
            var primaryEnrollment = await _primaryEnrollmentRepo.GetAllAsync();
            Console.WriteLine($"There are {primaryEnrollment.Count()} Enrollment records on primary database!");
            var backupEnrollment = await _backupEnrollmentRepo.GetAllAsync();
            Console.WriteLine($"There are {backupEnrollment.Count()} Enrollment records on backup database");

            //Convert Backup Database record of Enrollment into Dictionary for faster lookup
            var backupEnrollmentDict = backupEnrollment.ToDictionary(e => e.EnrollmentID);

            foreach (var enrollment in primaryEnrollment)
            {
                if(!backupEnrollmentDict.TryGetValue(enrollment.EnrollmentID, out var existingEnrollment))
                {
                    //Adding new enrollment if not present in backup database
                    var newEnrollment = new EnrollmentEntity
                    {
                        EnrollmentID = enrollment.EnrollmentID,
                        StudentID = enrollment.StudentID,
                        CourseID = enrollment.CourseID,
                        EnrollmentDate = enrollment.EnrollmentDate
                    };
                    Console.WriteLine($"Adding the record of Enrollment {enrollment.EnrollmentID} into backup database");
                    await _backupEnrollmentRepo.AddAsync(newEnrollment);
                }
                else
                {
                    //Check if any field have changed
                    if(existingEnrollment.StudentID != enrollment.StudentID || 
                        existingEnrollment.CourseID != enrollment.CourseID )
                    {
                        //Update the record
                        existingEnrollment.StudentID = enrollment.StudentID;
                        existingEnrollment.CourseID = enrollment.CourseID;
                        Console.WriteLine($"Updating the enrollment record of {existingEnrollment.EnrollmentID} in backup database!");
                        await _backupEnrollmentRepo.UpdateAsync(existingEnrollment.EnrollmentID, existingEnrollment);
                    }
                }
            }

            //Deleting enrollment record from backup database if not present in primary
            var primaryEnrollmentIDs = primaryEnrollment.Select(e => e.EnrollmentID).ToHashSet();
            var enrollmentToDelete = backupEnrollment.Where(e => !primaryEnrollmentIDs.Contains(e.EnrollmentID)).ToList();
            foreach (var enrollment in enrollmentToDelete)
            {
                Console.WriteLine($"Deleting the record of enrollment {enrollment.EnrollmentID} from backup database");
                await _backupEnrollmentRepo.DeleteAsync(enrollment.EnrollmentID);
            }
            Console.WriteLine("Synchronization on table Enrollment completed!");
        }

        public async Task SynchronizeGradeAsync()
        {
            //Fetching Grades from Both primary and backup database
            var primaryGrade = await _primaryGradeRepo.GetAllAsync();
            Console.WriteLine($"There are {primaryGrade.Count()} records on grades table in primary database");
            var backupGrade = await _backupGradeRepo.GetAllAsync();
            Console.WriteLine($"There are {backupGrade.Count()} records on grade table in backup database");

            //Convert backupdata to Dictionary for faster lookup
            var backupGradeDict = backupGrade.ToDictionary(g => g.GradeID);

            foreach(var grade in primaryGrade)
            {
                if(!backupGradeDict.TryGetValue(grade.GradeID ,out var existingGrade))
                {
                    //Adding new grade record on the table
                    var newGrade = new GradeEntity
                    {
                        GradeID = grade.GradeID,
                        StudentID = grade.StudentID,
                        CourseID = grade.CourseID,
                        Grade = grade.Grade
                    };
                    Console.WriteLine($"Adding grade record {grade.GradeID} on backup database!");
                    await _backupGradeRepo.AddAsync(newGrade);
                }
                else
                {
                    if(existingGrade.StudentID != grade.StudentID ||
                        existingGrade.CourseID != grade.CourseID ||
                        existingGrade.Grade != grade.Grade)
                    {
                        existingGrade.StudentID = grade.StudentID;
                        existingGrade.CourseID = grade.CourseID;
                        existingGrade.Grade = grade.Grade;
                        Console.WriteLine($"Updating the grade record of Grade {existingGrade.GradeID} on Backup database!");
                        await _backupGradeRepo.UpdateAsync(existingGrade.GradeID, existingGrade);
                    }
                }

                //Deleting the grade record if it only exist in backup database
                var primaryGradeIDs = primaryGrade.Select(g => g.GradeID).ToHashSet();
                var gradeToDelete = backupGrade.Where(g => !primaryGradeIDs.Contains(g.GradeID)).ToList();
                foreach (var gradeDel in gradeToDelete)
                {
                    Console.WriteLine($"Deleting record of Grade {gradeDel.GradeID} from backup database!");
                    _backupGradeRepo.DeleteAsync(gradeDel.GradeID);
                }
            }
            Console.WriteLine("Synchronization on table grade completed successfully!");
        }
    }
}
