using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Songs
{
    public class IndexModel : PageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public IndexModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        public IList<Song> Song { get;set; }
        public SongData SongD { get; set; }
        public int SongID { get; set; }
        public int CategoryID { get; set; }
        public async Task OnGetAsync(int? id, int? categoryID)
        {
            SongD = new SongData();

            SongD.Songs = await _context.Song
            .Include(b => b.Publisher)
            .Include(b => b.SongCategories)
            .ThenInclude(b => b.Category)
            .AsNoTracking()
            .OrderBy(b => b.Title)
            .ToListAsync();
            if (id != null)
            {
                SongID = id.Value;
                Song song = SongD.Songs
                .Where(i => i.ID == id.Value).Single();
                SongD.Categories = song.SongCategories.Select(s => s.Category);
            }
        }

    }
}
