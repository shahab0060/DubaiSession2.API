using DubaiSession2.API.Context;
using DubaiSession2.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DubaiSession2.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CancellController : ControllerBase
    {
        #region constructor

        private readonly ComXContext _context;
        public CancellController()
        {
            _context = new ComXContext();
        }

        #endregion

        public async Task<IActionResult>Index()
        {
            return Ok(await _context
                .CancellationPolicies
                .Select(a=> new CancelPolictyInformationViewModel()
                {
                    Id = a.Id,
                    Name = a.Name
                }).ToListAsync());
        }
    }
}
