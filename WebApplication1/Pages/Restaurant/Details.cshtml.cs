using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace WebApplication1.Pages.Restaurant
{
    public class DetailsModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public DetailsModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public CheckedIn CheckedIn { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkedIn = await _context.CheckedIn.FirstOrDefaultAsync(m => m.CheckedInId == id);

            if (checkedIn == null)
            {
                return NotFound();
            }
            else 
            {
                CheckedIn = checkedIn;
            }
            return Page();
        }
    }
}
