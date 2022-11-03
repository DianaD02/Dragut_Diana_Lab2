using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Dragut_Diana_Lab2.Data;
using Dragut_Diana_Lab2.Models;
using Dragut_Diana_Lab2.Models.ViewModels;

namespace Dragut_Diana_Lab2.Pages.Publishers
{
    public class IndexModel : PageModel
    {
        private readonly Dragut_Diana_Lab2.Data.Dragut_Diana_Lab2Context _context;

        public IndexModel(Dragut_Diana_Lab2.Data.Dragut_Diana_Lab2Context context)
        {
            _context = context;
        }

        public IList<Publisher> Publisher { get;set; } = default!;

        public PublisherIndexData PublisherData { get; set; }
        public int PublisherID { get; set; }
        public int BookID { get; set; }
        public async Task OnGetAsync(int? id, int? bookID )
        {   PublisherData=new PublisherIndexData();
            PublisherData.Publishers=await _context.Publisher
                .Include(i=> i.Books)
                   .ThenInclude(c =>c.Author)
                .OrderBy(i=>i.PublisherName)
                .ToListAsync();
            if (id != null)
            {
                PublisherID = id.Value;
                Publisher publishers = PublisherData.Publishers
                    .Where(i => i.ID == id.Value).Single();
                PublisherData.Books = publishers.Books;


            }
        }
    }
}
