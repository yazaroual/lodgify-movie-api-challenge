using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiApplication.Database.Entities;
using ApiApplication.Database.Repositories.Abstractions;
using ApiApplication.Dto;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ShowtimesController : ControllerBase
{
    private readonly IApiClientGrpc _apiClientGrpc;
    private readonly IAuditoriumsRepository _auditoriumsRepository;
    private readonly IShowtimesRepository _showtimesRepository;

    public ShowtimesController(IApiClientGrpc apiClientGrpc,
        IAuditoriumsRepository auditoriumsRepository,
        IShowtimesRepository showtimesRepository)
    {
        _apiClientGrpc = apiClientGrpc;
        _auditoriumsRepository = auditoriumsRepository;
        _showtimesRepository = showtimesRepository;
    }

    /// <summary>
    /// Get available movies
    /// </summary>
    /// <returns>A list of movies that you can create a show time for.</returns>
    [HttpGet]
    [Route("showtimes/movies")]
    public async Task<IActionResult> GetAvailableMovies()
    {
        //This endpoint could have been on it's own controller but as far as the requirements go, we only need movies to create showtimes
        //That's why it's here
        var movies = await _apiClientGrpc.GetAll();

        return Ok(new GetAvailableMoviesResponse(movies));
    }

    /// <summary>
    /// Get available aduitoriums
    /// </summary>
    /// <returns>A list of auditoriums.</returns>
    [HttpGet]
    [Route("showtimes/auditoriums")]
    public async Task<IActionResult> GetAvailableAuditoriums(CancellationToken cancellationToken)
    {
        //Same as for movies, we only require auditoriums to create showtimes so I preffered to keep it here

        var auditoriums = await _auditoriumsRepository.GetAllAsync(cancellationToken);

        return Ok(new GetAvailableAuditoriumsResponse(auditoriums.Select(x => x.Id).ToList()));
    }

    /// <summary>
    /// Create a showtime
    /// </summary>
    /// <param name="request">A specific movie and hour</param>
    /// <returns>The created showtime</returns>
    [HttpPost]
    [Route("showtimes")]
    public async Task<IActionResult> CreateShowtime([FromBody] CreateShowtimeRequest request, CancellationToken cancellationToken)
    {
        //We could add validation steps here to make sure that an auditorium is available at the requested time

        var show = new ShowtimeEntity
        {
            Movie = new MovieEntity { ImdbId = request.MovieId },
            SessionDate = request.SessionDate,
            AuditoriumId = request.AuditoriumId
        };

        var showtime = await _showtimesRepository.CreateShowtime(show, cancellationToken);

        return Ok(new CreateShowtimeResponse
        {
            Id = showtime.Id,
            MovieId = showtime.Movie.ImdbId,
            SessionDate = showtime.SessionDate,
            AuditoriumId = showtime.AuditoriumId
        });
    }
}
