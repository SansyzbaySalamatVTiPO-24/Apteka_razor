using System.ComponentModel.DataAnnotations;

namespace Apteka_razor.Data.Models
{
    public class Pharmacy
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
    }
}

