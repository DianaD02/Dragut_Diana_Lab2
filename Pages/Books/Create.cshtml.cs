using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dragut_Diana_Lab2.Data;
using Dragut_Diana_Lab2.Models;

namespace Dragut_Diana_Lab2.Pages.Books
{
    public class CreateModel : BookCategoriesPageModels
    {
        private readonly Dragut_Diana_Lab2.Data.Dragut_Diana_Lab2Context _context;

        public CreateModel(Dragut_Diana_Lab2.Data.Dragut_Diana_Lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var authorList = _context.Author.Select(x => new
            {
                x.Id,
                FullName = x.LastName + " " + x.FirstName
            });

            ViewData["AuthorID"] = new SelectList(authorList, "ID", "FullName");
            ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName");

            var book = new Book();
            book.BookCategory = new List<BookCategory>();

            PopulateAssignedCategoryData(_context, book);
            return Page();
        }

        [BindProperty]
        public Book Book { get; set;}

        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            var newBook = new Book();
            if (selectedCategories != null)
            {
                newBook.BookCategory = new List<BookCategory>();
                foreach (var cat in selectedCategories)
                {
                    var catToAdd = new BookCategory
                    {
                        CategoryID = int.Parse(cat)
                    };
                    newBook.BookCategory.Add(catToAdd);
                }
            }

            if (await TryUpdateModelAsync<Book>(
                newBook,
                "Book",
                i => i.Title,
                i => i.Author,
                i => i.Price,
                i => i.PublishingDate,
                i => i.PublisherID))
            {
                _context.Book.Add(newBook);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateAssignedCategoryData(_context, newBook);
            return Page();

        }

    }
}
