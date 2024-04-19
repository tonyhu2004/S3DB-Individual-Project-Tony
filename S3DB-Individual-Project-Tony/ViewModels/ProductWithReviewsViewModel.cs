namespace S3DB_Individual_Project_Tony.ViewModels;

public class ProductWithReviewsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public string Description { get; set; } = "";
    public decimal AverageRating { get; set; }
    
    public List<ReviewViewModel>? Reviews{ get; set; }
}