using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apteka_razor.Data.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Дата продажи")]
        public DateTime SaleDate { get; set; } = DateTime.Today;

        [Display(Name = "Общая сумма (decimal)")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? TotalPrice { get; set; } = 0;


        [Display(Name = "Общая сумма (double, если нужно старое поле)")]
        public double Total { get; set; } = 0;

        // 🔹 Внешние ключи
        [Required]
        public int EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public Employee? Employee { get; set; }  // Сделали nullable для безопасного Include

        [Required]
        public int CustomerId { get; set; }
         [Required]
 
        [ForeignKey(nameof(CustomerId))]
        public Customer? Customer { get; set; }  // Сделали nullable

        // 🔹 Связь один-ко-многим с SaleDetail
        public ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
    }
}
