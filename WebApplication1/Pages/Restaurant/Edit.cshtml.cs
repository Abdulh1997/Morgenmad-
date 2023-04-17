using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Model;

namespace WebApplication1.Pages.Restaurant
{
    public class EditModel : PageModel
    {
        private readonly WebApplication1.Data.ApplicationDbContext _context;

        public EditModel(WebApplication1.Data.ApplicationDbContext context)
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

            var checkedin =  await _context.CheckedIn.FirstOrDefaultAsync(m => m.CheckedInId == id);
            if (checkedin == null)
            {
                return NotFound();
            }
            CheckedIn = checkedin;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(CheckedIn).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckedInExists(CheckedIn.CheckedInId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CheckedInExists(long id)
        {
          return (_context.CheckedIn?.Any(e => e.CheckedInId == id)).GetValueOrDefault();
        }
    }
}
