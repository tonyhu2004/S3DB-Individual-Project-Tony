
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace S3DB_Individual_Project_Tony.RequestModels;

public class MessageRequest
{
    [Required]
    public string Chat_ID { get; set; }
    [Required]
    public string? Text { get; set; }
}

