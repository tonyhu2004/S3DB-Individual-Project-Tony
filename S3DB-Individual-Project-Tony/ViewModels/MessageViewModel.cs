namespace S3DB_Individual_Project_Tony.ViewModels;

public class MessageViewModel
{
    public int Id { get; set; }
    public int ChatId { get; set; }
    public string SenderUserId { get; set; } = "";
    public string? Text { get; set; }
    public DateTime SendDate { get; set; }
}