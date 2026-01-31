using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using evaluacionparcial1.Models;

namespace evaluacionparcial1.Controllers
{
    public class ArtistasController : Controller
    {
        private readonly MusicDbContext _context;

        public ArtistasController(MusicDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var artistas = await _context.Artistas.ToListAsync();
            return View(artistas);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = await _context.Artistas
                .Include(a => a.AlbumArtistas)
                .ThenInclude(aa => aa.Album)
                .FirstOrDefaultAsync(a => a.ArtistaId == id);

            if (artista == null)
            {
                return NotFound();
            }

            return View(artista);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Apellido,FechaNacimiento,Nacionalidad")] Artista artista)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(artista);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = await _context.Artistas.FindAsync(id);
            if (artista == null)
            {
                return NotFound();
            }
            return View(artista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArtistaId,Nombre,Apellido,FechaNacimiento,Nacionalidad")] Artista artista)
        {
            if (id != artista.ArtistaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artista);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistaExists(artista.ArtistaId))
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
            return View(artista);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = await _context.Artistas
                .FirstOrDefaultAsync(a => a.ArtistaId == id);

            if (artista == null)
            {
                return NotFound();
            }

            return View(artista);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artista = await _context.Artistas.FindAsync(id);
            if (artista != null)
            {
                _context.Artistas.Remove(artista);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistaExists(int id)
        {
            return _context.Artistas.Any(a => a.ArtistaId == id);
        }
    }
}
