using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
    public int ID { get; set; }

    [Required] [StringLength(25)] public string Name { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    [Required] [StringLength(255)] public string Description { get; set; }

    [Required] public string AccountId { get; set; }

    public ApplicationUser Account { get; set; }

    public List<ProductInformation>? ProductInformation { get; set; }

    public List<Review>? Reviews { get; set; }
    
    [NotMapped]
    [Column(TypeName = "decimal(10,2)")]
    public decimal AverageRating { get; set; }
}