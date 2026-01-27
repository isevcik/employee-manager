using EmployeeManager.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Data.Seed;

public static class JobCategoriesSeed
{
    public static void SeedJobCategories(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<JobCategory>().HasData(
            new JobCategory { Id = 1, Title = "Software Engineer" },
            new JobCategory { Id = 2, Title = "Product Manager" },
            new JobCategory { Id = 3, Title = "Project Manager" },
            new JobCategory { Id = 4, Title = "QA Engineer" },
            new JobCategory { Id = 5, Title = "DevOps Engineer" },
            new JobCategory { Id = 6, Title = "HR Specialist" },
            new JobCategory { Id = 7, Title = "Sales Representative" },
            new JobCategory { Id = 8, Title = "Marketing Manager" },
            new JobCategory { Id = 9, Title = "Business Analyst" },
            new JobCategory { Id = 10, Title = "Customer Support" }
        );
    }
}
