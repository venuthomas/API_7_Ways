using System.ComponentModel.DataAnnotations;

namespace vT.ApiDomains.Models;

public class Employee
{
    [Key]
    public int ID { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required int Age { get; set; }
}