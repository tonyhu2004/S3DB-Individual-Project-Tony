namespace S3DB_Individual_Project_Tony.ViewModels;

public class MessageViewModel
{
    public int ID { get; set; }
    public int Chat_ID { get; set; }
    public string SenderUser_ID { get; set; }
    public string? Text { get; set; }
    public DateTime SendDate { get; set; }
}
