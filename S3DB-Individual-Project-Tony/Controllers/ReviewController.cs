using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using S3DB_Individual_Project_Tony.CustomFilter;
using S3DB_Individual_Project_Tony.ViewModels;
using System.Security.Claims;
using Org.BouncyCastle.Asn1.Ocsp;
using S3DB_Individual_Project_Tony.RequestModels;

namespace S3DB_Individual_Project_Tony.Controllers;

[Authorize]
[ServiceFilter(typeof(CustomExceptionFilter))]
[ApiController]
[Route("[controller]")]
public class ReviewController : ControllerBase
{
    private readonly ReviewService _service;

    public ReviewController(ReviewService reviewService)
    {
        _service = reviewService;
    }

    [HttpPost("")]
    public ActionResult Post([FromBody] ReviewRequest reviewRequest)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        Review review = new Review
        {
            Rating = reviewRequest.Rating,
            Comment = reviewRequest.Comment,
            ProductId = reviewRequest.ProductId,
            UserId = userId!,
            PublishedDate = DateTime.UtcNow,
        };
        return Ok(_service.CreateReview(review));
    }
}