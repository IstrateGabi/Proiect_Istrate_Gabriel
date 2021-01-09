using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Songs
{
    public class EditModel : SongCategoriesPageModel

    {
        private readonly Proiect.Data.ProiectContext _context;

        public EditModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Song Song { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Song = await _context.Song
                .Include(b => b.Publisher)
                .Include(b => b.SongCategories).ThenInclude(b => b.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Song == null)
            {
                return NotFound();
            }
            PopulateAssignedCategoryData(_context, Song);

            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[]
selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }
            var songToUpdate = await _context.Song
            .Include(i => i.Publisher)
            .Include(i => i.SongCategories)
            .ThenInclude(i => i.Category)
            .FirstOrDefaultAsync(s => s.ID == id);
            if (songToUpdate == null)
            {
                return NotFound();
            }
            if (await TryUpdateModelAsync<Song>(
            songToUpdate,
            "Song",
            i => i.Title, i => i.Author,
            i => i.Price, i => i.PublishingDate, i => i.Publisher))
            {
                UpdateSongCategories(_context, selectedCategories, songToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            
            UpdateSongCategories(_context, selectedCategories, songToUpdate);
            PopulateAssignedCategoryData(_context, songToUpdate);
            return Page();
        }
    }
}
