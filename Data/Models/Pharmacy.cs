using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apteka_razor.Data.Models
{
    public class Pharmacy
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        // Навигационные свойства для связи
        public List<Employee> Employees { get; set; } = new();
        public List<Drug> Drugs { get; set; } = new();
    }
}
