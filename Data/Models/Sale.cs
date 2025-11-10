using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apteka_razor.Data.Models
{
    [Table("Sales")]
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? TotalPrice { get; set; } // nullable для старых записей

        public DateTime SaleDate { get; set; }

        public Employee? Employee { get; set; }
        public virtual List<SaleDetail> SaleDetails { get; set; } = new();
    }
}
