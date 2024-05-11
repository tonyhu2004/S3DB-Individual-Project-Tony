
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using S3DB_Individual_Project_Tony.CustomFilter;
using S3DB_Individual_Project_Tony.RequestModels;

namespace S3DB_Individual_Project_Tony;

[ServiceFilter(typeof(CustomExceptionFilter))]
[Authorize]
public class ChatHub : Hub
{
    private readonly ChatService _chatService;

    public ChatHub(ChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task AddToGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

    //public async Task RemoveFromGroup(string groupName)
    //{
    //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    //}

    public async Task SendMessage(MessageRequest messageRequest)
    {
        var userId = Context?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        Message message = new Message
        {
            Chat_ID = Convert.ToInt32(messageRequest.Chat_ID),
            Text = messageRequest.Text,
            SendDate = DateTime.Now,
        };

        message.SenderUser_ID = userId ?? "";

        _chatService.SendMessage(message);

        await Clients.Group(messageRequest.Chat_ID).SendAsync("ReceiveMessage", message);
    }
}
