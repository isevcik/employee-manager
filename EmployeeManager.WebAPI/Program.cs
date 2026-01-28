using EmployeeManager.Data;
using EmployeeManager.Mappings;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<EmployeeSeederService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    // Run migrations on startup
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();

    // Seed employees from JSON file
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
app.UseCors("AllowFrontend");
app.MapControllers();
app.Run();
