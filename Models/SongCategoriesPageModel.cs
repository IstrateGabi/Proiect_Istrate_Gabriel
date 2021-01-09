using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proiect.Data;


namespace Proiect.Models
{
    public class SongCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList;
        public void PopulateAssignedCategoryData(ProiectContext context,
        Song song)
        {
            var allCategories = context.Category;
            var songCategories = new HashSet<int>(
            song.SongCategories.Select(c => c.SongID));
            AssignedCategoryDataList = new List<AssignedCategoryData>();
            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = cat.ID,
                    Name = cat.CategoryName,
                    Assigned = songCategories.Contains(cat.ID)
                });
            }
        }
        public void UpdateSongCategories(ProiectContext context,
        string[] selectedCategories, Song songToUpdate)
        {
            if (selectedCategories == null)
            {
                songToUpdate.SongCategories = new List<SongCategory>();
                return;
            }
            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var songCategories = new HashSet<int>
            (songToUpdate.SongCategories.Select(c => c.Category.ID));
            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.ID.ToString()))
                {
                    if (!songCategories.Contains(cat.ID))
                    {
                        songToUpdate.SongCategories.Add(
                        new SongCategory
                        {
                            SongID = songToUpdate.ID,
                            CategoryID = cat.ID
                        });
                    }
                }
                else
                {
                    if (songCategories.Contains(cat.ID))
                    {
                        SongCategory courseToRemove
                        = songToUpdate
                        .SongCategories
                        .SingleOrDefault(i => i.CategoryID == cat.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}
