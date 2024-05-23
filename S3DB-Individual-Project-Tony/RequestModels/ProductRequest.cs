using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace S3DB_Individual_Project_Tony.RequestModels;

public class ProductRequest
{
    [Required] public string Name { get; set; } = "";
    
    [Range(0.1, 10000)]
    [Required] public decimal Price { get; set; }

    [Required] public string Description { get; set; } = "";
    
    [Required] public IFormFile? FormFile { get; set; } 
}