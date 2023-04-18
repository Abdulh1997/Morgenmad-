using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using WebApplication1.Hubs;
using WebApplication1.Model;

namespace WebApplication1.Pages.Reception
{
    public class CreateModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly IHubContext<NotificationHub, INotificationHub> _hubContext;

        public CreateModel(Data.ApplicationDbContext context, IHubContext<NotificationHub, INotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Reservations Reservations { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Reservations.Add(Reservations);
            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.Update();

            return RedirectToPage("./Index");
        }
    }
}
