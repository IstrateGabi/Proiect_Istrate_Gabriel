using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.Models
{
    public class SongData
    {
        public IEnumerable<Song> Songs { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<SongCategory> SongCategories { get; set; }
    }
}
