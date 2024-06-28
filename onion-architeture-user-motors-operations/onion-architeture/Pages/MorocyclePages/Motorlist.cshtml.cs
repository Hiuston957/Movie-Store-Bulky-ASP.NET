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
    public class MotorlistModel : PageModel
    {
        private readonly onion_architeture.Infrastructure.Data.ApplicationDbContext _context;

        public MotorlistModel(onion_architeture.Infrastructure.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Motorcycle> Motorcycle { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Motorcycle = await _context.Motorcycles.ToListAsync();
        }
    }
}
