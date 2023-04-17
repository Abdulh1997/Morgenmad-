﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Model;

namespace WebApplication1.Pages.Recepetion
{
    public class DeleteModel : PageModel
    {
        private readonly WebApplication1.Data.ApplicationDbContext _context;

        public DeleteModel(WebApplication1.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Reservations Reservations { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.Reservations == null)
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
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }
            var reservations = await _context.Reservations.FindAsync(id);

            if (reservations != null)
            {
                Reservations = reservations;
                _context.Reservations.Remove(Reservations);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}