namespace Core.Models;

public class Message
{
    public int ID { get; set; }
    public int ChatId { get; set; }
    public Chat Chat { get; set; }
    public string SenderUserId { get; set; }
    public ApplicationUser SenderUser { get; set; }
    public string? Text { get; set; }
    public DateTime SendDate { get; set; }

}