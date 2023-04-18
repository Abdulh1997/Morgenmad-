using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Model;

namespace WebApplication1.Pages.Restaurant
{
    public class DeleteModel : PageModel
    {
        private readonly WebApplication1.Data.ApplicationDbContext _context;

        public DeleteModel(WebApplication1.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public CheckedIn CheckedIn { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.CheckedIn == null)
            {
                return NotFound();
            }

            var checkedin = await _context.CheckedIn.FirstOrDefaultAsync(m => m.CheckedInId == id);

            if (checkedin == null)
            {
                return NotFound();
            }
            else 
            {
                CheckedIn = checkedin;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null || _context.CheckedIn == null)
            {
                return NotFound();
            }
            var checkedin = await _context.CheckedIn.FindAsync(id);

            if (checkedin != null)
            {
                CheckedIn = checkedin;
                _context.CheckedIn.Remove(CheckedIn);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
