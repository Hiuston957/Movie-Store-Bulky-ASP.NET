using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using onion_architeture.Infrastructure.Data;
using onion_architeture.Core.Entities;

namespace onion_architeture.Pages.MorocyclePages
{
    public class MotorEditModel : PageModel
    {
        private readonly onion_architeture.Infrastructure.Data.ApplicationDbContext _context;

        public MotorEditModel(onion_architeture.Infrastructure.Data.ApplicationDbContext context)
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

            var motorcycle =  await _context.Motorcycles.FirstOrDefaultAsync(m => m.Id == id);
            if (motorcycle == null)
            {
                return NotFound();
            }
            Motorcycle = motorcycle;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {


            _context.Attach(Motorcycle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MotorcycleExists(Motorcycle.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Motorlist");
        }

        private bool MotorcycleExists(int id)
        {
            return _context.Motorcycles.Any(e => e.Id == id);
        }
    }
}
