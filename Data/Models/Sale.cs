using System.ComponentModel.DataAnnotations;

namespace Apteka_razor.Data.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public DateTime Date { get; set; }  // Дата продажи

        public decimal TotalPrice { get; set; }

        public List<SaleDetail> SaleDetails { get; set; } = new();
    }
}
