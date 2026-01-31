using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace evaluacionparcial1.Models
{
    [Table("Artistas")]
    public class Artista
    {
        [Key]
        [Column("artista_id")]
        public int ArtistaId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(100)]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        [Column("fecha_nacimiento")]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "La nacionalidad es obligatoria")]
        [StringLength(100)]
        [Display(Name = "Nacionalidad")]
        public string Nacionalidad { get; set; } = string.Empty;

        [Display(Name = "Nombre Completo")]
        public string NombreCompleto => $"{Nombre} {Apellido}";

        public ICollection<AlbumArtista> AlbumArtistas { get; set; } = new List<AlbumArtista>();
    }
}
