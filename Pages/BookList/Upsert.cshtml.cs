using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDBContext _context;

        public UpsertModel(ApplicationDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
            Book = new Book();
            if (id == null)
            {
                //create
                return Page();
            }

            //update
            Book = await _context.Books.FirstOrDefaultAsync(u => u.Id == id);
            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {

                if (Book.Id == 0)
                {
                    _context.Books.Add(Book);
                }
                else
                {
                    _context.Books.Update(Book);
                }

                await _context.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}
