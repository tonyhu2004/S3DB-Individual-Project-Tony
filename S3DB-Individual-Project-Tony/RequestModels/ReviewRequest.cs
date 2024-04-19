using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace S3DB_Individual_Project_Tony.RequestModels;

public class ReviewRequest
{
    [Required]    
    public decimal Rating { get; set; }    

    [Required]    
    public string Comment { get; set; } = "";

    [Required]    
    public int ProductId { get; set; }
}