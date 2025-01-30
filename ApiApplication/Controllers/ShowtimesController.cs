
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ShowtimesController : ControllerBase
{
    /// <summary>
    /// Get available movies
    /// </summary>
    /// <returns>A list of movies that you can create a show time for.</returns>
    [HttpGet]
    [Route("showtimes/movies")]
    public async Task<IActionResult> GetAvailableMovies()
    {
        return Ok(new GetAvailableMoviesResponse());
    }

    /// <summary>
    /// Create a showtime
    /// </summary>
    /// <param name="request">A specific movie and hour</param>
    /// <returns>The created showtime</returns>
    [HttpPost]
    [Route("showtimes")]
    public async Task<IActionResult> CreateShowtime([FromBody] CreateShowtimeRequest request)
    {
        return Ok();
    }
}


public class CreateShowtimeRequest
{
    public DateTime StartsAt { get; set; }
    public string MovieId { get; set; }
    public DateTime EndsAt { get; set; }
    public int AuditoriumId { get; set; }
    public decimal Price { get; set; }
}

//Create a GetMoviesResponse class
public class GetAvailableMoviesResponse
{
    public List<MovieDto> Movies { get; set; }
}

public class MovieDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
}