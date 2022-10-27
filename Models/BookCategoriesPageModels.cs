using Microsoft.AspNetCore.Mvc.RazorPages;
using Dragut_Diana_Lab2;
using Dragut_Diana_Lab2.Data;

namespace Dragut_Diana_Lab2.Models
{
    public class BookCategoriesPageModels : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList;
        public void PopulateAssignedCategoryData(Dragut_Diana_Lab2Context context, Book book)
        {
            var allCategories = context.Category; var bookCategories = new HashSet<int>(book.BookCategory.Select(c => c.CategoryID)); //
            AssignedCategoryDataList = new List<AssignedCategoryData>();
            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = cat.Id,
                    Name = cat.CategoryName,
                    Assigned = bookCategories.Contains(cat.Id)
                });
            }
        }
        public void UpdateBookCategories(Dragut_Diana_Lab2Context context, string[] selectedCategories, Book bookToUpdate)
        {
            if (selectedCategories == null) { bookToUpdate.BookCategory = new List<BookCategory>(); return; }
            var selectedCategoriesHS = new HashSet<string>(selectedCategories); var bookCategories = new HashSet<int>(bookToUpdate.BookCategory.Select(c => c.Category.Id)); foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.Id.ToString())) { if (!bookCategories.Contains(cat.Id)) { bookToUpdate.BookCategory.Add(new BookCategory { BookID = bookToUpdate.ID, CategoryID = cat.Id }); } }
                else
                {
                    if (bookCategories.Contains(cat.Id))
                    {
                        BookCategory courseToRemove = bookToUpdate.BookCategory
            .SingleOrDefault(i => i.CategoryID == cat.Id);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}