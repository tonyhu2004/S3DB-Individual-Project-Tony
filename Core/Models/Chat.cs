using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Core.Models;

public class Chat
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
    public int Id { get; set; }

    [Required] [StringLength(8000)] public string User1Id { get; set; } = "";
    [Required] [StringLength(8000)] public string User2Id { get; set; } = "";
    public IdentityUser? User1 { get; set; }
    public IdentityUser? User2 { get; set; }
    public List<Message>? Messages { get; set; }
}