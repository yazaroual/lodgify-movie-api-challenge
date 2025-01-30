
using System;
namespace ApiApplication.Dto
{
    public class CreateShowtimeResponse
    {
        public int Id { get; set; }
        public DateTime SessionDate { get; set; }
        public string MovieId { get; set; }
        public int AuditoriumId { get; set; }
    }
}
