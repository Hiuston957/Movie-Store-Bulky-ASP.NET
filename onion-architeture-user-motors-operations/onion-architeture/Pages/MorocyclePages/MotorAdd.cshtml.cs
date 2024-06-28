using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using onion_architeture.Infrastructure.Data;
using onion_architeture.Core.Entities;

namespace onion_architeture.Pages.MorocyclePages
{
    public class MotorAddModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public MotorAddModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Motorcycle Motorcycle { get; set; } = default!;

        [TempData]
        public string Message { get; set; }

        [TempData]
        public bool IsSuccess { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                _context.Motorcycles.Add(Motorcycle);
                await _context.SaveChangesAsync();
                IsSuccess = true;
                Message = "Motorcycle added successfully!";
            }
            catch (Exception ex)
            {
                IsSuccess = false;
                Message = $"Error: {ex.Message}";
            }

            // Powrót na stronę z komunikatem o wyniku operacji
            return RedirectToPage("./Motorlist");
        }
    }
}