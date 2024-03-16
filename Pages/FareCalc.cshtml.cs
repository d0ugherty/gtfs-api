using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GtfsApi.Models;
using System.Threading.Tasks;

namespace GtfsApi.Pages
{
    public class FareCalcModel : PageModel
    {
        private readonly GtfsContext _context;
        public List<Agency> Agencies { get; set; }

        public FareCalcModel(GtfsContext dbContext)
        {
            _context = dbContext;
        }
        public async Task OnGetAsync()
        {
            Agencies = await _context.Agencies.ToListAsync();
            
        }
    }
}
