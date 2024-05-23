using Core.Models;

namespace Core.Interfaces;

public interface IChatRepository
{
    Chat? GetChatBy(string userId1, string userId2);
    Chat CreateChat(string userId1, string userId2);
    void SendMessage(Message message);
}