using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Core.Models;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
    public int Id { get; set; }

    [Required] [StringLength(25)] public string Name { get; set; } = "";

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    [Required] [StringLength(255)] public string Description { get; set; } = "";

    [Required] [StringLength(8000)] public string UserId { get; set; } = "";

    public ApplicationUser? User { get; set; }

    public List<ProductInformation>? ProductInformation { get; set; }

    public List<Review>? Reviews { get; set; }

    [NotMapped]
    [Column(TypeName = "decimal(10,2)")]
    public decimal AverageRating { get; set; }

    [NotMapped] [Required] public IFormFile? FormFile { get; set; }

    [NotMapped] public string? ImageUrl { get; set; }
}