using System;

namespace ApiApplication.Dto
{
    public class MovieDto
    {
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string Stars { get; set; }
        public string ReleaseYear { get; set; }
    }
}