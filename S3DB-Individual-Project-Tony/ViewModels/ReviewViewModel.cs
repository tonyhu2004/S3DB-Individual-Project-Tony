﻿namespace S3DB_Individual_Project_Tony.ViewModels;

public class ReviewViewModel
{
    public int Id { get; set; }
    public decimal Rating { get; set; }
    public string Comment { get; set; } = "";
    public int ProductId { get; set; }
}