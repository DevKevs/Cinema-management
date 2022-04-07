using System.ComponentModel.DataAnnotations;

namespace Movie_app.Client.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public string? hola { get; set; }
        [Required(ErrorMessage = "Codigo necesario...")]
        public string? SeatCode { get; set; }
        public bool IsAvailable { get; set; }
        [Required(ErrorMessage = "Numero de sala necesario...")]
        public int? RoomNumber { get; set; }
    }
}
