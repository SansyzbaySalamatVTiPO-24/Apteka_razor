namespace Apteka_razor.Data.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public DateTime? SaleDate { get; set; }   // ✅ допускает null
        public decimal? TotalPrice { get; set; }  // ✅ допускает null
        public int? EmployeeId { get; set; }      // ✅ допускает null
        public Employee? Employee { get; set; }
    }
}
