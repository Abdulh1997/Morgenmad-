using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Hubs;
using WebApplication1.Model;

namespace WebApplication1.Pages.Restaurant
{
    public class EditModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly IHubContext<NotificationHub, INotificationHub> _hubContext;

        public EditModel(Data.ApplicationDbContext context, IHubContext<NotificationHub, INotificationHub> hubContext)
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

            var checkedIn =  await _context.CheckedIn.FirstOrDefaultAsync(m => m.CheckedInId == id);

            if (checkedIn == null)
            {
                return NotFound();
            }
            CheckedIn = checkedIn;
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
                await _hubContext.Clients.All.Update();
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
