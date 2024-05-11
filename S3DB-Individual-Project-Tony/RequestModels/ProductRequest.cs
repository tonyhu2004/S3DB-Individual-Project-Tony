using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace S3DB_Individual_Project_Tony.RequestModels;

public class ProductRequest
{
    [Required] public string Name { get; set; } = "";

    [Required] public decimal Price { get; set; }

    [Required] public string Description { get; set; } = "";
    
    [Required] public IFormFile FormFile { get; set; } 
}