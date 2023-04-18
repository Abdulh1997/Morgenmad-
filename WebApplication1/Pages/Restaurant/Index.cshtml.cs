using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace WebApplication1.Pages.Restaurant
{
    [Authorize("IsWaiter")]
    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public IndexModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CheckedIn> CheckedIn { get;set; } = default!;
        public DateTime Date { get; set; } = DateTime.Today;

        public async Task OnGetAsync()
        {
            CheckedIn = await _context.CheckedIn.ToListAsync();
        }
    }
}
