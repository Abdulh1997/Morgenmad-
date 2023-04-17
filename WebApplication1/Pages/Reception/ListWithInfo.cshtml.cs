using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace WebApplication1.Pages.Reception
{
    public class ListWithInfoModel : PageModel
    {
        private readonly WebApplication1.Data.ApplicationDbContext _context;

        public ListWithInfoModel(WebApplication1.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CheckedIn> info { get; set; } = default!;
        public DateTime Date { get; set; } = DateTime.Today;

        public async Task OnGetAsync()
        {
            info = await _context.CheckedIn.ToListAsync();
        }
    }
}
