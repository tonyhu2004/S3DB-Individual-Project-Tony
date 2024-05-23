using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Message
{
    public int Id { get; set; }
    public int ChatId { get; set; }
    public Chat? Chat { get; set; }
    [StringLength(8000)] public string SenderUserId { get; set; } = "";
    public ApplicationUser? SenderUser { get; set; }
    [StringLength(8000)] public string Text { get; set; }  = "";
    public DateTime SendDate { get; set; }

}