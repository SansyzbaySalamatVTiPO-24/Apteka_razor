// Models/Employee.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apteka_razor.Data.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "Seller";

        public int PharmacyId { get; set; } // В БД NOT NULL, поэтому убираем nullable

        [ForeignKey("PharmacyId")]
        public Pharmacy Pharmacy { get; set; } = null!; // В БД NOT NULL
    }
}