using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using WebApplication1.Data;
using WebApplication1.Hubs;
using WebApplication1.Model;

namespace WebApplication1.Pages.Restaurant
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<NotificationHub, INotificationHub> _hubContext;

        public CreateModel(ApplicationDbContext context, IHubContext<NotificationHub, INotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CheckedIn CheckedIn { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.CheckedIn == null || CheckedIn == null)
            {
                return Page();
            }

            _context.CheckedIn.Add(CheckedIn);
            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.Update();

            return RedirectToPage("./Index");
        }
    }
}
