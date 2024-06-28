using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using onion_architeture.Core.Entities;
using onion_architeture.Infrastructure.Data;

namespace onion_architeture.Pages.UserAdmiistrationPages
{
    public class DetailsUserMotorModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsUserMotorModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Client Client { get; set; } = default!;
        public List<Motorcycle> Motorcycles { get; set; } = new List<Motorcycle>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("./Userlist");
            }

            Client = await _context.Clients
                .Include(c => c.ClientAndMotorcycles)
                .ThenInclude(cm => cm.Motorcycle)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (Client == null)
            {
                return RedirectToPage("./Userlist");
            }

            foreach (var cm in Client.ClientAndMotorcycles)
            {
                Motorcycles.Add(cm.Motorcycle);
            }

            return Page();
        }
    }
}
