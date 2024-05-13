namespace S3DB_Individual_Project_Tony.ViewModels;

public class ChatViewModel
{
    public int ID { get; set; }
    public string CurrentUserId { get; set; }
    public List<MessageViewModel>? MessageViewModels { get; set; }
}
