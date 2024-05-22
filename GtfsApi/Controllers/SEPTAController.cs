using Gtfs.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GtfsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SEPTAController : ControllerBase
    {

        private readonly GtfsContext _context;
        
        public SEPTAController(GtfsContext context)
        {
            _context = context;
        }
    }
}
