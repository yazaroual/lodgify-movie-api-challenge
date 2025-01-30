
using System;
namespace ApiApplication.Dto
{
    public class CreateShowtimeRequest
    {
        public DateTime SessionDate { get; set; }
        public string MovieId { get; set; }
        public int AuditoriumId { get; set; }
    }
}
