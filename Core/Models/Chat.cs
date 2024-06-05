using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class Chat
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
    public int Id { get; set; }

    [Required] [StringLength(8000)] public string User1Id { get; set; } = "";
    [Required] [StringLength(8000)] public string User2Id { get; set; } = "";
    public ApplicationUser? User1 { get; set; }
    public ApplicationUser? User2 { get; set; }
    public List<Message>? Messages { get; set; }
}