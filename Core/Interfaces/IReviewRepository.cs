using Core.Models;

namespace Core.Interfaces
{
    public interface IReviewRepository
    {
        IEnumerable<Review> GetReviewsBy(string userId);
        IEnumerable<Review> GetReviewsBy(int productId);
        Review? GetReviewBy(int id);
        Review? GetReviewBy(int productId, string userId);
        bool CreateReview(Review review);
        bool UpdateReview(int id, Review review);
    }
}
