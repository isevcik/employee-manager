namespace EmployeeManager.DTOs;

public class AddressDto
{
    public int Id { get; set; }
    public string Street { get; set; } = default!;
    public string ZipCode { get; set; } = default!;
    public string City { get; set; } = default!;
    public int CountryId { get; set; }
}
