using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Songs
{
    public class CreateModel : SongCategoriesPageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public CreateModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
            var song = new Song();
            song.SongCategories = new List<SongCategory>();
            PopulateAssignedCategoryData(_context, song);
            return Page();
        }

        [BindProperty]
        public Song Song { get; set; }

        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            var newSong = new Song();
            if (selectedCategories != null)
            {
                newSong.SongCategories = new List<SongCategory>();
                foreach (var cat in selectedCategories)
                {
                    var catToAdd = new SongCategory
                    {
                        CategoryID = int.Parse(cat)
                    };
                    newSong.SongCategories.Add(catToAdd);
                }
            }
            if (await TryUpdateModelAsync<Song>(
            newSong,
            "Song",
            i => i.Title, i => i.Author,
            i => i.Price, i => i.PublishingDate, i => i.PublisherID))
            {
                _context.Song.Add(newSong);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            PopulateAssignedCategoryData(_context, newSong);
            return Page();
        }

    }
}
