using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Hubs;
using WebApplication1.Model;

namespace WebApplication1.Pages.Reception
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
      public Reservations Reservations { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservations = await _context.Reservations.FirstOrDefaultAsync(m => m.ReservationsID == id);

            if (reservations == null)
            {
                return NotFound();
            }
            else 
            {
                Reservations = reservations;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var reservations = await _context.Reservations.FindAsync(id);

            if (reservations != null)
            {
                Reservations = reservations;
                _context.Reservations.Remove(Reservations);
                await _context.SaveChangesAsync();
                await _hubContext.Clients.All.Update();
            }

            return RedirectToPage("./Index");
        }
    }
}
