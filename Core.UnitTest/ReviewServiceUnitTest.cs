using Core.Interfaces;
using Core.Models;
using Core.Services;
using Moq;

namespace Core.UnitTest;

public class ReviewServiceUnitTest
{
    [Fact]
    public void CreateReview_WithValidReview_ReturnsTrue()
    {
        var expected = new Review
        {
            Id = 1,
            Rating = 3,
            Comment = "Pretty mid",
            ProductId = 2,
            UserId = "a3",
            PublishedDate = DateTime.Now
        };
        var mock = new Mock<IReviewRepository>();
        mock.Setup(p => p.CreateReview(expected)).Returns(true);
        var reviewService = new ReviewService(mock.Object);

        var actual = reviewService.CreateReview(expected);

        Assert.True(actual);
    }

    [Fact]
    public void CreateReview_WithInvalidReview_ThrowsInvalidOperationException()
    {
        var expected = new Review
        {
            Id = 1,
            Rating = 3,
            ProductId = 2,
            UserId = "a3",
            PublishedDate = DateTime.Now
        };
        var mock = new Mock<IReviewRepository>();
        mock.Setup(p => p.CreateReview(expected)).Returns(true);
        var reviewService = new ReviewService(mock.Object);

        Assert.Throws<InvalidOperationException>(() => reviewService.CreateReview(expected));
    }

    [Fact]
    public void CreateReview_AlreadyExists_ThrowsArgumentException()
    {
        var expected = new Review
        {
            Id = 1,
            Rating = 3,
            Comment = "Pretty mid",
            ProductId = 2,
            UserId = "a3",
            PublishedDate = DateTime.Now
        };
        var mock = new Mock<IReviewRepository>();
        mock.Setup(p => p.GetReviewBy(expected.ProductId, expected.UserId)).Returns(expected);
        mock.Setup(p => p.CreateReview(expected)).Returns(true);
        var reviewService = new ReviewService(mock.Object);

        Assert.Throws<ArgumentException>(() => reviewService.CreateReview(expected));
    }

    [Fact]
    public void UpdateReview_WithValidReview_ReturnsTrue()
    {
        var expected = new Review
        {
            Id = 1,
            Rating = 3,
            Comment = "Pretty mid",
            ProductId = 2,
            UserId = "a3",
            PublishedDate = DateTime.Now
        };
        var mock = new Mock<IReviewRepository>();
        mock.Setup(p => p.GetReviewBy(expected.Id)).Returns(expected);
        mock.Setup(p => p.UpdateReview(expected.Id, expected)).Returns(true);
        var reviewService = new ReviewService(mock.Object);

        var actual = reviewService.UpdateReview(expected.Id, expected);

        Assert.True(actual);
    }

    [Fact]
    public void UpdateReview_WithInvalidReview_ThrowsInvalidOperationException()
    {
        var expected = new Review
        {
            Id = 1,
            Rating = 3,
            ProductId = 2,
            UserId = "a3",
            PublishedDate = DateTime.Now
        };
        var mock = new Mock<IReviewRepository>();
        mock.Setup(p => p.GetReviewBy(expected.Id)).Returns(expected);
        var reviewService = new ReviewService(mock.Object);

        Assert.Throws<InvalidOperationException>(() => reviewService.UpdateReview(expected.Id, expected));
    }

    [Fact]
    public void UpdateReview_InValidId_ThrowsArgumentException()
    {
        var expected = new Review
        {
            Id = 1,
            Rating = 3,
            Comment = "Pretty mid",
            ProductId = 2,
            UserId = "a3",
            PublishedDate = DateTime.Now
        };
        var mock = new Mock<IReviewRepository>();
        mock.Setup(p => p.GetReviewBy(expected.ProductId, expected.UserId)).Returns(null as Review);
        var reviewService = new ReviewService(mock.Object);

        Assert.Throws<ArgumentException>(() => reviewService.UpdateReview(expected.Id, expected));
    }
}