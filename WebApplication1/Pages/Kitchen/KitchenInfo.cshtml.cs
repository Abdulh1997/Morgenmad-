using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Model;

namespace WebApplication1.Pages.Kitchen
{
    public class KitchenInfoModel : PageModel
    {

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public class InputModel
        {
            public DateTime Date { get; set; } = DateTime.Now.Date;
        }


        private readonly WebApplication1.Data.ApplicationDbContext _context;

        public KitchenInfoModel(WebApplication1.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {

            var list = _context.Reservations.Where(r => r.Date.Date == Input.Date.Date).ToList();
            var list2 = _context.CheckedIn.Where(r => r.Date.Date == Input.Date.Date).ToList();

            int totalAdult = 0;
            int totalChildren = 0;
            int checkedInAdult = 0;
            int checkedInChildren = 0;

            foreach (Reservations model in list)
            {
                totalAdult += model.NumberOfAdults;
                totalChildren += model.NumberOfChildren;
            }

            foreach (CheckedIn model in list2)
            {
                checkedInAdult += model.NumberOfAdults;
                checkedInChildren += model.NumberOfChildren;
            }



            ViewData["Adult"] = $"{totalAdult}";
            ViewData["Children"] = $"{totalChildren}";
            ViewData["Total"] = $"{totalAdult + totalChildren}";
            ViewData["CheckedInAdult"] = $"{checkedInAdult}";
            ViewData["CheckedInChildren"] = $"{checkedInChildren}";
            ViewData["CheckedInTotal"] = $"{checkedInAdult + checkedInChildren}";
            ViewData["NotCheckedIn"] = $"{(totalAdult + totalChildren) - (checkedInAdult + checkedInChildren)}";
        }
    }
}
