using Apteka_razor.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


[Table("SaleDetail")] // ← важно!
public class SaleDetail
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int SaleId { get; set; }

    [ForeignKey(nameof(SaleId))]
    public Sale Sale { get; set; } = null!;

    [Required]
    public int DrugId { get; set; }

    [ForeignKey(nameof(DrugId))]
    public Drug Drug { get; set; } = null!;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Required]
    public int Quantity { get; set; }
}
