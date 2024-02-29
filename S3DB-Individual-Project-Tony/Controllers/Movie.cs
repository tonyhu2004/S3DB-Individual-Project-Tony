using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace S3DB_Individual_Project_Tony.Controllers
{
    public class Movie
    {
        private int iD;
        private string? title;
        private string? language;
        private string? coverImageUrl;
        private string? synopsis;
        private DateTime releaseDate;
        private string? director;

        public int ID
        {
            get => iD; set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("You've submitted an incomplete Movie.");
                }
                iD = value;
            }
        }
        public string? Title
        {
            get => title; set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("You've submitted an incomplete Movie.");
                }
                title = value;
            }
        }
        public string? Language
        {
            get => language;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("You've submitted an incomplete Movie.");
                }
                language = value;
            }
        }
        public string? CoverImageUrl
        {
            get => coverImageUrl;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("You've submitted an incomplete Movie.");
                }
                coverImageUrl = value;
            }
        }
        public string? Synopsis
        {
            get => synopsis; set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("You've submitted an incomplete Movie.");
                }
                synopsis = value;
            }
        }
        public DateTime ReleaseDate
        {
            get => releaseDate; set
            {
                if (value == default)
                {
                    throw new ArgumentException("You've submitted an incomplete Movie.");
                }
                releaseDate = value;
            }
        }
        public string? Director
        {
            get => director; set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("You've submitted an incomplete Movie.");
                }
                director = value;
            }
        }
    }
}
