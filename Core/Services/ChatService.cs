using Core.Interfaces;
using Core.Models;

namespace Core.Services;

public class ChatService
{
    private readonly IChatRepository _repository;

    public ChatService(IChatRepository chatRepository)
    {
        _repository = chatRepository;
    }

    public Chat GetOrCreateChatBy(string user1Id, string user2Id)
    {
        var existingChat = _repository.GetChatBy(user1Id, user2Id);
        if (existingChat == null) return _repository.CreateChat(user1Id, user2Id);
        return existingChat;
    }

    public void SendMessage(Message message)
    {
        if (!IsMessageComplete(message)) throw new InvalidOperationException("Message isn't complete");
        _repository.SendMessage(message);
    }

    private static bool IsMessageComplete(Message message)
    {
        if (message.ChatId is <= 0 or > 5 || string.IsNullOrWhiteSpace(message.SenderUserId) ||
            string.IsNullOrWhiteSpace(message.Text)) return false;
        return true;
    }
}