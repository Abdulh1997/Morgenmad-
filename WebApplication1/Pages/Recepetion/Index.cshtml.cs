using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Hubs;
using WebApplication1.Model;

namespace WebApplication1.Pages.Recepetion
{

    [Authorize("IsReceptionist")]
    public class IndexModel : PageModel
    {
        //public IHubContext<NotificationHub> _NotificationHub;

        //public IndexModel(IHubContext<NotificationHub> NotificationHub)
        //{
        //    _NotificationHub = NotificationHub;
        //}


        private readonly WebApplication1.Data.ApplicationDbContext _context;

        public IndexModel(WebApplication1.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Reservations> Reservations { get;set; } = default!;
        public DateTime Date { get; set; } = DateTime.Today;


        public async Task OnGetAsync()
        {
           if (_context.Reservations != null)
           {
                    Reservations = await _context.Reservations.ToListAsync();
                   // await _NotificationHub.Clients.All.SendAsync("ReceiveMessage");
           }
        }
    }
}
