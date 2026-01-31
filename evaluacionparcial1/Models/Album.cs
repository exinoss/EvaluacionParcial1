using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace evaluacionparcial1.Models
{
    [Table("Albumes")]
    public class Album
    {
        [Key]
        [Column("album_id")]
        public int AlbumId { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(200)]
        [Display(Name = "Título")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El género es obligatorio")]
        [StringLength(50)]
        [Display(Name = "Género")]
        public string Genero { get; set; } = string.Empty;

        [Required(ErrorMessage = "El año de lanzamiento es obligatorio")]
        [Column("año_lanzamiento")]
        [Display(Name = "Año de Lanzamiento")]
        [Range(1900, 2100, ErrorMessage = "El año debe estar entre 1900 y 2100")]
        public int AnioLanzamiento { get; set; }

        [Required(ErrorMessage = "La discográfica es obligatoria")]
        [StringLength(150)]
        [Display(Name = "Discográfica")]
        public string Discografica { get; set; } = string.Empty;

        public ICollection<AlbumArtista> AlbumArtistas { get; set; } = new List<AlbumArtista>();
    }
}
