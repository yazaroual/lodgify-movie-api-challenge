using System.Collections.Generic;

namespace ApiApplication.Dto
{
    public class GetAvailableAuditoriumsResponse
    {
        public GetAvailableAuditoriumsResponse(List<int> auditoriums)
        {
            Auditoriums = auditoriums;
        }

        /// <summary>
        /// A list of auditoriums ids
        /// </summary>
        public List<int> Auditoriums { get; set; }
    }
}