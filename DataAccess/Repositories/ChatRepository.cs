using Core.Interfaces;
using Core.Models;
using DataAccess.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class ChatRepository: IChatRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ChatRepository(DbContextOptions<ApplicationDbContext> options)
    {
        _dbContext = new ApplicationDbContext(options);
    }

    public Chat? GetChatBy(string user1Id, string user2Id)
    {
        var chat = _dbContext.Chats
            .Include(c => c.Messages)
            .Include(c => c.User1)
            .Include(c => c.User2)
            .FirstOrDefault(c => (c.User1Id == user1Id && c.User2Id == user2Id) || (c.User1Id == user2Id && c.User2Id == user1Id));

        return chat;
    }

    public Chat CreateChat(string user1Id, string user2Id)
    {
        var chat = new Chat
        {
            User1Id = user1Id,
            User2Id = user2Id,
        };
        _dbContext.Chats.Add(chat);
        _dbContext.SaveChanges();

        return chat;
    }

    public void SendMessage(Message message)
    {
        _dbContext.Messages.Add(message);
        _dbContext.SaveChanges();
    }
}