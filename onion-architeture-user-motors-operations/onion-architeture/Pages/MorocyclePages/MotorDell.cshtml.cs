using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using onion_architeture.Infrastructure.Data;
using onion_architeture.Core.Entities;

namespace onion_architeture.Pages.MorocyclePages
{
    public class MotorDellModel : PageModel
    {
        private readonly onion_architeture.Infrastructure.Data.ApplicationDbContext _context;

        public MotorDellModel(onion_architeture.Infrastructure.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Motorcycle Motorcycle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorcycle = await _context.Motorcycles.FirstOrDefaultAsync(m => m.Id == id);

            if (motorcycle == null)
            {
                return NotFound();
            }
            else
            {
                Motorcycle = motorcycle;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorcycle = await _context.Motorcycles.FindAsync(id);
            if (motorcycle != null)
            {
                Motorcycle = motorcycle;
                _context.Motorcycles.Remove(Motorcycle);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Motorlist");
        }
    }
}
