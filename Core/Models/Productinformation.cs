using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class ProductInformation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
    public int Id { get; set; }

    [Required] [StringLength(25)] public string Category { get; set; } = "";

    [Required] [StringLength(25)] public string Value { get; set; } = "";

    [Required] public int ProductId { get; set; }
    public Product? Product { get; set; }

}