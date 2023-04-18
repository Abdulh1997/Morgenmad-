using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Hubs;
using WebApplication1.Model;

namespace WebApplication1.Pages.Reception
{
    public class EditModel : PageModel
    {
        private readonly WebApplication1.Data.ApplicationDbContext _context;
        private readonly IHubContext<NotificationHub, INotificationHub> _hubContext;


        public EditModel(WebApplication1.Data.ApplicationDbContext context, IHubContext<NotificationHub, INotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [BindProperty]
        public Reservations Reservations { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservations =  await _context.Reservations.FirstOrDefaultAsync(m => m.ReservationsID == id);
            if (reservations == null)
            {
                return NotFound();
            }
            Reservations = reservations;
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

            _context.Attach(Reservations).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await _hubContext.Clients.All.Update();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationsExists(Reservations.ReservationsID))
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

        private bool ReservationsExists(long id)
        {
          return (_context.Reservations?.Any(e => e.ReservationsID == id)).GetValueOrDefault();
        }
    }
}
