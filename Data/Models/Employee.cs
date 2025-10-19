namespace Apteka_razor.Data.Models

{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";
        public string Role { get; set; } = "Seller";
        public int PharmacyId { get; set; }
        public Pharmacy? Pharmacy { get; set; }
    }
}
