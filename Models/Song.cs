using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proiect.Models
{
    public class Song
    {
        public int ID { get; set; }

        [Required, StringLength(150, MinimumLength = 3)]

        [Display(Name = "Song Title")]
        public string Title { get; set; }

        [Display(Name = "Artist")]
        public string Author { get; set; }
        
        [Range(1, 300)]

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishingDate { get; set; }
        public int PublisherID { get; set; }
        public Publisher Publisher { get; set; }

        [Display(Name = "Category")]

        public ICollection<SongCategory> SongCategories { get; set; }
    }
}
