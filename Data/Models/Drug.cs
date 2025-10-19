namespace Apteka_razor.Data.Models
{
    public class Drug
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Manufacturer { get; set; }     // nullable
        public decimal? Price { get; set; }           // nullable
        public DateTime? ExpirationDate { get; set; } // nullable
        public int? PharmacyId { get; set; }          // nullable
        public Pharmacy? Pharmacy { get; set; }
    }
}

