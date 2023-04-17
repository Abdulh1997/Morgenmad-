using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Model;

namespace WebApplication1.Pages.Restaurant
{

    [Authorize("IsWaiter")]
    public class IndexModel : PageModel
    {
        private readonly WebApplication1.Data.ApplicationDbContext _context;

        public IndexModel(WebApplication1.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CheckedIn> CheckedIn { get;set; } = default!;
        public DateTime Date { get; set; } = DateTime.Today;

        public async Task OnGetAsync()
        {
            if (_context.CheckedIn != null)
            {
                CheckedIn = await _context.CheckedIn.ToListAsync();
            }
        }
    }
}
