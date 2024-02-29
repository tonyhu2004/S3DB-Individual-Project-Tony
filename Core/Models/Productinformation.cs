using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Productinformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
        public int ID { get; set; }

        [Required]
        [StringLength(25)]
        public string Category { get; set; }

        [Required]
        [StringLength(25)]
        public string Value { get; set; }

        [Required]
        public int ProductId { get; set; }

    }
}
