using System;
using System.ComponentModel.DataAnnotations;

namespace Movie_app.Client.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string? hola { get; set; }
        [Required(ErrorMessage = "Titulo necesario...")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Fecha de estreno necesaria...")]
        public DateTime ReleaseDate { get; set; }
        [Required(ErrorMessage = "Genero necesario...")]
        public string? Gender { get; set; }
        [Required(ErrorMessage = "Precio del ticket necesario...")]
        public decimal? TicketPrice { get; set; }
        [Required(ErrorMessage = "Duracion de la pelicula necesaria...")]
        public string? Duration { get; set; }
        public string? Photo { get; set; }
        [Required(ErrorMessage = "Sinopsis necesaria...")]
        public string? Sinopsis { get; set; }
    }
}
