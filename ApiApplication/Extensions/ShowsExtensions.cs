using System.Runtime.CompilerServices;
using ApiApplication.Dto;
using ProtoDefinitions;

namespace ApiApplication.Extensions
{

    public static class ShowsExtensions
    {
        public static MovieDto ToMovieDto(this showResponse show)
        {
            return new MovieDto
            {
                ImdbId = show.Id,
                Title = show.Title,
                Stars = show.ImDbRating,
                ReleaseYear = show.Year
            };
        }
    }
}