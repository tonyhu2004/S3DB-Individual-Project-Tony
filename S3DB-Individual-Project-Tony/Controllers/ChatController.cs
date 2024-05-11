using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using S3DB_Individual_Project_Tony.CustomFilter;
using S3DB_Individual_Project_Tony.ViewModels;
using System.Security.Claims;
using Org.BouncyCastle.Asn1.Ocsp;
using S3DB_Individual_Project_Tony.RequestModels;

namespace S3DB_Individual_Project_Tony.Controllers;

[Authorize]
[ServiceFilter(typeof(CustomExceptionFilter))]
[ApiController]
[Route("[controller]")]
public class ChatController : ControllerBase
{
    private readonly ChatService _service;

    public ChatController(ChatService chatService)
    {
        _service = chatService;
    }

    [HttpGet("{id}")]
    public IActionResult GetChatBy(string userId1, string userId2)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var chat = _service.GetOrCreateChatBy(userId1, userId2);

        ChatViewModel chatViewModel = new ChatViewModel
        {
            ID = chat.ID,
            CurrentUser_ID = userId ?? "",
            MessageViewModels = chat.Messages.Select(m => new MessageViewModel
            {
                ID = m.ID,
                Chat_ID = chat.ID,
                SenderUser_ID = m.SenderUser_ID,
                Text = m.Text,
                SendDate = m.SendDate,
            }).ToList()
        };
        
        return Ok(chatViewModel);
    }
}