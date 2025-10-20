// Models/Pharmacy.cs
using System.ComponentModel.DataAnnotations;

namespace Apteka_razor.Data.Models
{
    public class Pharmacy
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public List<Employee> Employees { get; set; } = new();
    }
}