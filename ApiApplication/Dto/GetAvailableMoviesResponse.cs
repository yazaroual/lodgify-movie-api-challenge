using System.Collections.Generic;

namespace ApiApplication.Dto
{
    public class GetAvailableMoviesResponse
    {
        public GetAvailableMoviesResponse(List<MovieDto> movies)
        {
            Movies = movies;
        }

        public List<MovieDto> Movies { get; set; }
    }
}
