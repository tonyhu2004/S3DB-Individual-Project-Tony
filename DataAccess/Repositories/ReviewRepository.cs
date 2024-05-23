using Core.Interfaces;
using Core.Models;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ReviewRepository(DbContextOptions<ApplicationDbContext> options)
    {
        _dbContext = new ApplicationDbContext(options);
    }

    public IEnumerable<Review> GetReviewsBy(string userId)
    {
        return _dbContext.Reviews
            .Where(r => r.UserId == userId)
            .ToList();
    }

    public IEnumerable<Review> GetReviewsBy(int productId)
    {
        return _dbContext.Reviews
            .Where(r => r.ProductId == productId)
            .ToList();
    }

    public Review? GetReviewBy(int id)
    {
        return _dbContext.Reviews
            .FirstOrDefault(r => r.Id == id);
    }

    public bool CreateReview(Review review)
    {
        var entity = _dbContext.Reviews.FirstOrDefault(r=> r.UserId == review.UserId && r.ProductId == review.ProductId);
        if (entity != null) return false;

        _dbContext.Reviews.Add(review);
        _dbContext.SaveChanges();
        return true;
    }

    public bool UpdateReview(int id, Review review)
    {
        var entity = _dbContext.Reviews.FirstOrDefault(p => p.Id == id);
        if (entity == null) return false;

        entity.Rating = review.Rating;
        entity.Comment = review.Comment;
        entity.PublishedDate = review.PublishedDate;

        _dbContext.SaveChanges();
        return true;
    }

    public Review? GetReviewBy(int productId, string userId)
    {
        return _dbContext.Reviews
            .FirstOrDefault(r => r.UserId == userId && r.ProductId == productId);
    }
}