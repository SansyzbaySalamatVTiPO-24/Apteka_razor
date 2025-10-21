using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apteka_razor.Data.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Дата продажи")]
        public DateTime SaleDate { get; set; } = DateTime.Today; // ✅ правильное свойство

        [Display(Name = "Общая сумма (decimal)")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; } = 0; // ✅ decimal — для денег

        [Display(Name = "Общая сумма (double, если нужно старое поле)")]
        public double Total { get; set; } = 0; // ✅ если в БД есть столбец Total (double)

        // Внешние ключи
        [Required]
        public int EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }
    }
}
