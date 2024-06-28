using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using onion_architeture.Core.Entities;
using onion_architeture.Infrastructure.Data;

namespace onion_architeture.Pages.MorocyclePages
{
    public class InsuranceDetailsModel : PageModel
    {
        private readonly onion_architeture.Infrastructure.Data.ApplicationDbContext _context;

        public InsuranceDetailsModel(onion_architeture.Infrastructure.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Insurance Insurance { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("./Motorlist");
            }

            var insurance = await _context.Insurance.FirstOrDefaultAsync(m => m.MotorcycleId == id);
            if (insurance == null)
            {
                return RedirectToPage("./Motorlist");
            }
            else
            {
                Insurance = insurance;
            }
            return Page();
        }
    }
}
