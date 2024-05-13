
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace S3DB_Individual_Project_Tony.RequestModels;

public class MessageRequest
{
    public string ChatId { get; set; } = "";
    public string? Text { get; set; }
}

