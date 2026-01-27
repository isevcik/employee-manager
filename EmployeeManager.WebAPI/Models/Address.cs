using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManager.Models;

public class Address
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Street { get; set; } = default!;

    [Required, MaxLength(20)]
    public string ZipCode { get; set; } = default!;

    [Required, MaxLength(100)]
    public string City { get; set; } = default!;

    [ForeignKey("Country")]
    public int CountryId { get; set; }
    public Country Country{ get; set; } = default!;
}
