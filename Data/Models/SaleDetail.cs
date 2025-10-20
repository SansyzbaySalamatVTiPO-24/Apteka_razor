// Models/SaleDetail.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apteka_razor.Data.Models
{
    public class SaleDetail
    {
        [Key]
        public int Id { get; set; }

        public int SaleId { get; set; }
        public int DrugId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        [ForeignKey("SaleId")]
        public Sale Sale { get; set; } = null!;

        [ForeignKey("DrugId")]
        public Drug Drug { get; set; } = null!;
    }
}