using StudentManagement.Application;
using StudentManagement.Infrastructure;
using StudentManagement.Presentation;
using Serilog;
using StudentManagement.Application.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Allowing react to interact with backend API
var corsPolicy = "AllowReactApp";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy, policy =>
    {
        policy
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin();
    });
});
//

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresentation();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Enable CORS globally
app.UseCors(corsPolicy);

app.UseSerilogRequestLogging();
app.MapControllers();

app.UseHttpsRedirection();

app.Run("http://192.168.110.224:5066");
