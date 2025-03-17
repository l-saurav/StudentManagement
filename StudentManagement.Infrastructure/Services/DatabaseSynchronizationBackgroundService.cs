using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Infrastructure.Services
{
    public class DatabaseSynchronizationBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly TimeSpan _syncInterval = TimeSpan.FromMinutes(5); //Execute this background Service every five minute
        public DatabaseSynchronizationBackgroundService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("I am inside of the background task");
                try
                {
                    using (var scope = _serviceScopeFactory.CreateScope()) // Create a Scoped service inside the Singleton
                    {
                        Console.WriteLine("Changing the scope");
                        var syncService = scope.ServiceProvider.GetRequiredService<ISynchronizationService>(); // Resolve Scoped service
                        Console.WriteLine("Scoped changed Successfully");
                        await syncService.SynchronizeStudentsAsync();
                        await syncService.SynchronizeCoursesAsync();
                        await syncService.SynchronizeEnrollmentAsync();
                        await syncService.SynchronizeGradeAsync();
                        Console.WriteLine("Syncronized Service called sucessfully without error");
                    }
                }
                catch(Exception e)
                {
                    if (e.InnerException != null)
                    {
                        Console.WriteLine($"Inner Exception: {e.InnerException.Message}");
                        if (e.InnerException.InnerException != null)
                        {
                            Console.WriteLine($"Inner Inner Exception: {e.InnerException.InnerException.Message}");
                        }
                    }
                    Console.WriteLine($"Periodic Synchronization Failed: {e.Message}");
                }
                await Task.Delay(_syncInterval, stoppingToken);
            }
        }
    }
}
