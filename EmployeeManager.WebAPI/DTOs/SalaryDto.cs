namespace EmployeeManager.DTOs;

public class SalaryDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime From { get; set; }
    public DateTime? To { get; set; }
}
