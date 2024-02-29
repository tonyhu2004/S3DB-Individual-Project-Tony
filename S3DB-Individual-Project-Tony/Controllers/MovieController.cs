using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.IO;

namespace S3DB_Individual_Project_Tony.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        [HttpGet("Details/{id}")]
        public ActionResult Details(int id)
        {
            Movie movie = new Movie();
            using (MySqlConnection connection = new MySqlConnection("Server=ShopHop;Database=MovieReview;Uid=root;Pwd=tony1234;"))
            {
                MySqlCommand command = new MySqlCommand("", connection)
                {
                    CommandText = "SELECT ID, Title, Language, CoverImageUrl, Synopsis, ReleaseDate, Director " +
                                  "FROM Movie " +
                                  "WHERE ID = @ID" // Adjusted SQL syntax
                };

                connection.Open();
                command.Parameters.AddWithValue("@ID", id);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        movie.ID = (int)reader["ID"];
                        movie.Title = (string)reader["Title"];
                        movie.Language = (string)reader["Language"];
                        movie.CoverImageUrl = (string)reader["CoverImageUrl"];
                        movie.Synopsis = (string)reader["Synopsis"];
                        movie.ReleaseDate = (DateTime)reader["ReleaseDate"];
                        movie.Director = (string)reader["Director"];
                    }
                }
            }
            return Ok(movie);
        }
    }
}
