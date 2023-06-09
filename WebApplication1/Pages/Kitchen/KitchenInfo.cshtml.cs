using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

            var totalAdult = 0;
            var totalChildren = 0;
            var checkedInAdult = 0;
            var checkedInChildren = 0;

            foreach (var model in list)
            {
                totalAdult += model.NumberOfAdults;
                totalChildren += model.NumberOfChildren;
            }

            foreach (var model in list2)
            {
                checkedInAdult += model.NumberOfAdults;
                checkedInChildren += model.NumberOfChildren;
            }

            var totalNotCheckedIn = (totalAdult + totalChildren) - (checkedInAdult + checkedInChildren);

            if (totalNotCheckedIn <= 0)
            {
                totalNotCheckedIn = 0;
            }

            ViewData["Adult"] = $"{totalAdult}";
            ViewData["Children"] = $"{totalChildren}";
            ViewData["Total"] = $"{totalAdult + totalChildren}";
            ViewData["CheckedInAdult"] = $"{checkedInAdult}";
            ViewData["CheckedInChildren"] = $"{checkedInChildren}";
            ViewData["CheckedInTotal"] = $"{checkedInAdult + checkedInChildren}";
            ViewData["NotCheckedIn"] = $"{totalNotCheckedIn}";
        }
    }
}
