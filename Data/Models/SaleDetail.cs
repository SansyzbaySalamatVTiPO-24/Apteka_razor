using Apteka_razor.Data.Models;
namespace Apteka_razor.Data.Models
{

    public class SaleDetail
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public int DrugId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }  // decimal, не double?

        public Sale Sale { get; set; }
        public Drug Drug { get; set; }
    }
}

