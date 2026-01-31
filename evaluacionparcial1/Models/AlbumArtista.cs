using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace evaluacionparcial1.Models
{
    [Table("AlbumArtistas")]
    public class AlbumArtista
    {
        [Key]
        public int Id { get; set; }

        [Column("album_id")]
        public int AlbumId { get; set; }

        [ForeignKey("AlbumId")]
        public Album Album { get; set; } = null!;

        [Column("artista_id")]
        public int ArtistaId { get; set; }

        [ForeignKey("ArtistaId")]
        public Artista Artista { get; set; } = null!;
    }
}
