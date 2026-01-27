using EmployeeManager.Data;
using EmployeeManager.Mappings;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<EmployeeSeederService>();

var app = builder.Build();

// Optional seeding of employees from JSON file
if (args.Contains("--seed-employees"))
{
    using var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<EmployeeSeederService>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    var seedFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "OptionalSeed", "employees.json");

    logger.LogInformation("Seeding employees from: {SeedFilePath}", seedFilePath);

    try
    {
        await seeder.SeedEmployeesFromJsonAsync(seedFilePath);
        logger.LogInformation("Employee seeding completed successfully");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Failed to seed employees");
        return;
    }
}

// Swagger configuration
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
