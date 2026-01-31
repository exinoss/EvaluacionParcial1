using Microsoft.EntityFrameworkCore;

namespace evaluacionparcial1.Models
{
    public class MusicDbContext : DbContext
    {
        public MusicDbContext(DbContextOptions<MusicDbContext> options) : base(options)
        {
        }

        public DbSet<Album> Albumes { get; set; }
        public DbSet<Artista> Artistas { get; set; }
        public DbSet<AlbumArtista> AlbumArtistas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AlbumArtista>()
                .HasOne(aa => aa.Album)
                .WithMany(a => a.AlbumArtistas)
                .HasForeignKey(aa => aa.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AlbumArtista>()
                .HasOne(aa => aa.Artista)
                .WithMany(a => a.AlbumArtistas)
                .HasForeignKey(aa => aa.ArtistaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AlbumArtista>()
                .HasIndex(aa => new { aa.AlbumId, aa.ArtistaId })
                .IsUnique();
        }
    }
}
