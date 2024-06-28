using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using onion_architeture.Core.Entities;
using onion_architeture.Infrastructure.Data;

namespace onion_architeture.Pages.UserAdmiistrationPages
{
    public class UserMotorDellModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public UserMotorDellModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ClientAndMotorcycle ClientAndMotorcycle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? clientId, int? motorcycleId)
        {
            if (clientId == null || motorcycleId == null)
            {
                return NotFound();
            }

            ClientAndMotorcycle = await _context.ClientMotorcycles
                .Include(cm => cm.Client)
                .Include(cm => cm.Motorcycle)
                .FirstOrDefaultAsync(cm => cm.ClientId == clientId && cm.MotorcycleId == motorcycleId);

            if (ClientAndMotorcycle == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? clientId, int? motorcycleId)
        {
            if (clientId == null || motorcycleId == null)
            {
                return NotFound();
            }

            ClientAndMotorcycle = await _context.ClientMotorcycles
                .FirstOrDefaultAsync(cm => cm.ClientId == clientId && cm.MotorcycleId == motorcycleId);

            if (ClientAndMotorcycle != null)
            {
                _context.ClientMotorcycles.Remove(ClientAndMotorcycle);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./DetailsUserMotor", new { id = clientId });
        }
    }
}
