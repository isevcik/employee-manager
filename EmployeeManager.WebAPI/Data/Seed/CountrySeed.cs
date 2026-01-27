using EmployeeManager.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Data.Seed;

public static class CountriesSeed
{
    public static void SeedCountries(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>().HasData(
            new Country { Id = 1, Name = "Albania" },
            new Country { Id = 2, Name = "Andorra" },
            new Country { Id = 3, Name = "Armenia" },
            new Country { Id = 4, Name = "Austria" },
            new Country { Id = 5, Name = "Azerbaijan" },
            new Country { Id = 6, Name = "Belarus" },
            new Country { Id = 7, Name = "Belgium" },
            new Country { Id = 8, Name = "Bosnia and Herzegovina" },
            new Country { Id = 9, Name = "Bulgaria" },
            new Country { Id = 10, Name = "Croatia" },
            new Country { Id = 11, Name = "Cyprus" },
            new Country { Id = 12, Name = "Czechia (Czech Republic)" },
            new Country { Id = 13, Name = "Denmark" },
            new Country { Id = 14, Name = "Estonia" },
            new Country { Id = 15, Name = "Finland" },
            new Country { Id = 16, Name = "France" },
            new Country { Id = 17, Name = "Georgia" },
            new Country { Id = 18, Name = "Germany" },
            new Country { Id = 19, Name = "Greece" },
            new Country { Id = 20, Name = "Hungary" },
            new Country { Id = 21, Name = "Iceland" },
            new Country { Id = 22, Name = "Ireland" },
            new Country { Id = 23, Name = "Italy" },
            new Country { Id = 24, Name = "Kazakhstan" },
            new Country { Id = 25, Name = "Kosovo" },
            new Country { Id = 26, Name = "Latvia" },
            new Country { Id = 27, Name = "Liechtenstein" },
            new Country { Id = 28, Name = "Lithuania" },
            new Country { Id = 29, Name = "Luxembourg" },
            new Country { Id = 30, Name = "Malta" },
            new Country { Id = 31, Name = "Moldova" },
            new Country { Id = 32, Name = "Monaco" },
            new Country { Id = 33, Name = "Montenegro" },
            new Country { Id = 34, Name = "Netherlands" },
            new Country { Id = 35, Name = "North Macedonia" },
            new Country { Id = 36, Name = "Norway" },
            new Country { Id = 37, Name = "Poland" },
            new Country { Id = 38, Name = "Portugal" },
            new Country { Id = 39, Name = "Romania" },
            new Country { Id = 40, Name = "Russia" },
            new Country { Id = 41, Name = "San Marino" },
            new Country { Id = 42, Name = "Serbia" },
            new Country { Id = 43, Name = "Slovakia" },
            new Country { Id = 44, Name = "Slovenia" },
            new Country { Id = 45, Name = "Spain" },
            new Country { Id = 46, Name = "Sweden" },
            new Country { Id = 47, Name = "Switzerland" },
            new Country { Id = 48, Name = "Turkey" },
            new Country { Id = 49, Name = "Ukraine" },
            new Country { Id = 50, Name = "United Kingdom" },
            new Country { Id = 51, Name = "Vatican City" }
        );
    }
}
