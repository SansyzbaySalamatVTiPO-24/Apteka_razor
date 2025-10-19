namespace Apteka_razor.Data.Models
{
    public class SaleDetail
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public Sale? Sale { get; set; }
        public int DrugId { get; set; }
        public Drug? Drug { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
