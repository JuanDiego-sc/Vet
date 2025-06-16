using API.Middleware;
using Application.AppointmentDetails.Queries;
using Application.Core;
using Application.Diseases.Validators;
using Application.Medicines.Validators;
using Application.Pets.Validators;
using FluentValidation;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options => 
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors();
builder.Services.AddMediatR(x =>
{
    x.RegisterServicesFromAssemblyContaining<GetDetailList.Handler>();
    x.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
builder.Services.AddValidatorsFromAssemblyContaining<CreatePetValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateMedicineValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateDiseaseValidator>();
builder.Services.AddTransient<ExceptionMiddleware>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithOrigins("http://localhost:3003", "https://localhost:3003"));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
    await DbInitializer.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration and seeding data");
}

app.MapControllers();

app.Run();
