using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.SignalR;
using S3DB_Individual_Project_Tony.RequestModels;

namespace S3DB_Individual_Project_Tony.Hub;

public class ChatHub : Hub<IChatHubClient>
{
    private readonly ChatService _chatService;

    public ChatHub(ChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task AddToGroup(MessageRequest messageRequest)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, messageRequest.ChatId);
    }

    //public async Task RemoveFromGroup(string groupName)
    //{
    //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    //}

    public async Task SendMessage(Message message)
    {
        await Clients.Group(Convert.ToString(message.ChatId)).ReceiveMessage(message);
    }

    // public async Task SendMessageAll(MessageRequest messageRequest)
    // {
    //     var userId = Context?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    //
    //     Message message = new Message
    //     {
    //         ChatId = Convert.ToInt32(messageRequest.ChatId),
    //         Text = messageRequest.Text,
    //         SendDate = DateTime.Now,
    //     };
    //
    //     message.SenderUserId = userId ?? "";
    //
    //     chatService.SendMessage(message);
    //
    //     await Clients.All.SendAsync("messageReceived", messageRequest.ChatId, message);
    // }
}