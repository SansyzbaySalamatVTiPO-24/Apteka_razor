using System.ComponentModel.DataAnnotations;

namespace Apteka_razor.Data.Models
{
    public class Drug
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        public string? Manufacturer { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ExpirationDate { get; set; }

        public int StockQuantity { get; set; } = 0;
    }
}
