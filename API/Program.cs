using API.Middleware;
using API.SignalR;
using Application.AppointmentDetails.Queries;
using Application.Core;
using Application.Diseases.Validators;
using Application.Interfaces;
using Application.Medicines.Validators;
using Application.Pets.Validators;
using Domain;
using FluentValidation;
using Infrastructure.Photos;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//AddControllers configure the way to API controllers work
builder.Services.AddControllers( options =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    //Filters allows to add a new rule 
    options.Filters.Add(new AuthorizeFilter(policy));   
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options => 
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors();
builder.Services.AddSignalR();
builder.Services.AddMediatR(x =>
{
    x.RegisterServicesFromAssemblyContaining<GetDetailList.Handler>();
    x.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddScoped<IUserAccessor, UserAccessor>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
builder.Services.AddValidatorsFromAssemblyContaining<CreatePetValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateMedicineValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateDiseaseValidator>();
builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.Configure<CloudinarySettings>(builder.Configuration
.GetSection("CloudinaryCredentials"));

builder.Services.AddIdentityApiEndpoints<User>(options =>
{
    options.User.RequireUniqueEmail = true;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("SignalRUser", policy =>
    {
        policy.RequireAuthenticatedUser();
    });

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials() //request with tokens are allowed
    .WithOrigins("http://localhost:3003", "https://localhost:3003"));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//*First the user authenticate an then authorize to the endpoint
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGroup("api").MapIdentityApi<User>();
app.MapHub<BlogHub>("/chat")
    .RequireAuthorization("SignalRUser");

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<AppDbContext>();
    var userManager = services.GetRequiredService<UserManager<User>>();
    await context.Database.MigrateAsync();
    await DbInitializer.SeedData(context, userManager);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration and seeding data");
}

app.Run();
