using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace WebApplication1.Pages.Reception
{
    [Authorize("IsReceptionist")]
    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public IndexModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Reservations> Reservations { get; set; } = default!;
        public DateTime Date { get; set; } = DateTime.Today;


        public async Task OnGetAsync()
        {
            Reservations = await _context.Reservations.ToListAsync();
        }
    }
}
