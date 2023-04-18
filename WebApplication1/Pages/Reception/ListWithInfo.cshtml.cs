using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace WebApplication1.Pages.Reception
{
    public class ListWithInfoModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public ListWithInfoModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CheckedIn> Info { get; set; } = default!;
        public DateTime Date { get; set; } = DateTime.Today;

        public async Task OnGetAsync()
        {
            Info = await _context.CheckedIn.ToListAsync();
        }
    }
}
