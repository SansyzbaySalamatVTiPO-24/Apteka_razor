// Models/Drug.cs
using System.ComponentModel.DataAnnotations;

namespace Apteka_razor.Data.Models
{
    public class Drug
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }

        // Только эти свойства есть в БД
        // Manufacturer и ExpirationDate временно удалены
    }
}