using Core.Models;

namespace S3DB_Individual_Project_Tony.Hub;

public interface IChatHubClient
{
    Task ReceiveMessage(Message message);
    Task AddToGroup(string chatId);
}