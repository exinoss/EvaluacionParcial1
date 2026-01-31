using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using evaluacionparcial1.Models;

namespace evaluacionparcial1.Controllers
{
    public class AlbumesController : Controller
    {
        private readonly MusicDbContext _context;

        public AlbumesController(MusicDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var albumes = await _context.Albumes
                .Include(a => a.AlbumArtistas)
                .ThenInclude(aa => aa.Artista)
                .ToListAsync();
            return View(albumes);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albumes
                .Include(a => a.AlbumArtistas)
                .ThenInclude(aa => aa.Artista)
                .FirstOrDefaultAsync(a => a.AlbumId == id);

            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new AlbumViewModel
            {
                ArtistasDisponibles = await _context.Artistas.ToListAsync()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlbumViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(viewModel.Album);
                await _context.SaveChangesAsync();

                if (viewModel.ArtistasSeleccionados != null && viewModel.ArtistasSeleccionados.Any())
                {
                    foreach (var artistaId in viewModel.ArtistasSeleccionados)
                    {
                        var albumArtista = new AlbumArtista
                        {
                            AlbumId = viewModel.Album.AlbumId,
                            ArtistaId = artistaId
                        };
                        _context.AlbumArtistas.Add(albumArtista);
                    }
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            viewModel.ArtistasDisponibles = await _context.Artistas.ToListAsync();
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albumes
                .Include(a => a.AlbumArtistas)
                .FirstOrDefaultAsync(a => a.AlbumId == id);

            if (album == null)
            {
                return NotFound();
            }

            var viewModel = new AlbumViewModel
            {
                Album = album,
                ArtistasSeleccionados = album.AlbumArtistas.Select(aa => aa.ArtistaId).ToList(),
                ArtistasDisponibles = await _context.Artistas.ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AlbumViewModel viewModel)
        {
            if (id != viewModel.Album.AlbumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewModel.Album);

                    var existingRelations = await _context.AlbumArtistas
                        .Where(aa => aa.AlbumId == id)
                        .ToListAsync();
                    _context.AlbumArtistas.RemoveRange(existingRelations);

                    if (viewModel.ArtistasSeleccionados != null && viewModel.ArtistasSeleccionados.Any())
                    {
                        foreach (var artistaId in viewModel.ArtistasSeleccionados)
                        {
                            var albumArtista = new AlbumArtista
                            {
                                AlbumId = viewModel.Album.AlbumId,
                                ArtistaId = artistaId
                            };
                            _context.AlbumArtistas.Add(albumArtista);
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(viewModel.Album.AlbumId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            viewModel.ArtistasDisponibles = await _context.Artistas.ToListAsync();
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albumes
                .Include(a => a.AlbumArtistas)
                .ThenInclude(aa => aa.Artista)
                .FirstOrDefaultAsync(a => a.AlbumId == id);

            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _context.Albumes.FindAsync(id);
            if (album != null)
            {
                _context.Albumes.Remove(album);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(int id)
        {
            return _context.Albumes.Any(a => a.AlbumId == id);
        }
    }
}
