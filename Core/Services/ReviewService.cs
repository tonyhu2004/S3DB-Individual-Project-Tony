using Core.Interfaces;
using Core.Models;

namespace Core.Services;

public class ReviewService
{
    private readonly IReviewRepository _repository;

    public ReviewService(IReviewRepository reviewRepository)
    {
        _repository = reviewRepository;
    }

    public IEnumerable<Review> GetReviewsBy(string userId)
    {
        return _repository.GetReviewsBy(userId);
    }

    public IEnumerable<Review> GetReviewsBy(int productId)
    {
        return _repository.GetReviewsBy(productId);
    }

    public Review? GetReviewBy(int id)
    {
        var existingReview = _repository.GetReviewBy(id);
        return existingReview ?? throw new ArgumentException("Review doesn't exist");
    }

    public bool CreateReview(Review review)
    {
        if (!IsReviewComplete(review)) throw new InvalidOperationException("Review isn't complete");
        var existingReview = _repository.GetReviewBy(review.ProductId, review.UserId);
        if (existingReview != null) throw new ArgumentException("Review already exists");
        return _repository.CreateReview(review);
    }

    public bool UpdateReview(int id, Review review)
    {
        if (!IsReviewComplete(review)) throw new InvalidOperationException("Review isn't complete");
        var existingReview = _repository.GetReviewBy(id);
        if (existingReview == null) throw new ArgumentException("Review doesn't exist");
        return _repository.UpdateReview(id, review);
    }
    private static bool IsReviewComplete(Review review)
    {
        if (review.Rating is <= 0 or > 5 || string.IsNullOrWhiteSpace(review.Comment) ||
            review.ProductId <= 0 || string.IsNullOrWhiteSpace(review.UserId)) return false;
        return true;
    }
}