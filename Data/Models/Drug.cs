namespace Apteka_razor.Data.Models
{
    public class Drug
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int Quantity { get; set; } // ← только это поле, без ? и без StockQuantity
        public string? Manufacturer { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? PharmacyId { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public Pharmacy? Pharmacy { get; set; }
    }
}
