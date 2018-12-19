using System;

namespace GighubApp.Dtos
{
    public class GigDto
    {
        public int GigId { get; set; }
        public bool IsCanceled { get; set; }
        public UserDto Artist { get; set; }
        public DateTime GigDate { get; set; }
        public string Venue { get; set; }
        public GenreDto Genre { get; set; }
    }
}