using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Hubs;
using WebApplication1.Model;

namespace WebApplication1.Pages.Restaurant
{
    public class DeleteModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly IHubContext<NotificationHub, INotificationHub> _hubContext;
        
        public DeleteModel(Data.ApplicationDbContext context, IHubContext<NotificationHub, INotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var checkedIn = await _context.CheckedIn.FindAsync(id);

            if (checkedIn != null)
            {
                CheckedIn = checkedIn;
                _context.CheckedIn.Remove(CheckedIn);
                await _context.SaveChangesAsync();
                await _hubContext.Clients.All.Update();
            }

            return RedirectToPage("./Index");
        }
    }
}
