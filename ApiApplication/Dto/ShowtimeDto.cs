using System;

namespace ApiApplication.Dto
{
    public class ShowtimeDto
    {
        public int Id { get; set; }
        public string MovieImdbId { get; set; }
        public DateTime SessionDate { get; set; }
        public int AuditoriumId { get; set; }
    }
}