// Models/Sale.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apteka_razor.Data.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Column("Employeeld")] // Обратите внимание на название столбца в БД
        public int EmployeeId { get; set; }

        [Column("SaleDate")]
        public DateTime? Date { get; set; }

        [Column("TotalPrice")]
        public decimal? TotalPrice { get; set; } // В БД допускает NULL

        // Дополнительные столбцы из БД, которых нет в модели
        public int CustomerId { get; set; } // Обязательное поле в БД

        [Column("Total")]
        public double Total { get; set; }  // В БД тип float, в C# - double

        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }

        public List<SaleDetail> SaleDetails { get; set; } = new();
    }
}