using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class Review
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "decimal(2,1)")]
    public decimal Rating { get; set; }

    [Required] [StringLength(8000)] public string Comment { get; set; } = "";

    [Required] public int ProductId { get; set; }

    public Product? Product { get; set; }

    [Required] [StringLength(8000)] public string UserId { get; set; } = "";

    public ApplicationUser? User { get; set; }

    [Required] public DateTime PublishedDate { get; set; }
}