using System.Collections.Generic;

namespace evaluacionparcial1.Models
{
    public class AlbumViewModel
    {
        public Album Album { get; set; } = new Album();
        public List<int> ArtistasSeleccionados { get; set; } = new List<int>();
        public List<Artista> ArtistasDisponibles { get; set; } = new List<Artista>();
    }
}
