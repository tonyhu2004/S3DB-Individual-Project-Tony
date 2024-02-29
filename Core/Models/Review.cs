using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Core.Models
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [Column(TypeName = "decimal(2,1)")]
        public decimal Rating { get; set; }

        [Required]
        [StringLength(255)]
        public string Comment { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public string AccountId { get; set; }
        public ApplicationUser Account { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }
    }
}
