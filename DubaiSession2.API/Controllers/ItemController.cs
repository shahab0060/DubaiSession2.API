using DubaiSession2.API.Context;
using DubaiSession2.API.Entities;
using DubaiSession2.API.ViewModels.Item;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DubaiSession2.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        #region constructor

        private readonly ComXContext _context;
        public ItemController()
        {
            _context = new ComXContext();
        }

        #endregion

        [HttpGet("index/{userId}")]
        public async Task<IActionResult> Index(long userId)
        {
            List<ShowItemBaseInformationViewModel> items = await _context
                .Items
                .Where(a => a.UserId == userId)
                .Include(a => a.ItemPrices)
                .OrderBy(a => a.ItemPrices.Count(ip => ip.Date >= DateTime.Now))
                .Select(a => new ShowItemBaseInformationViewModel()
                {
                    Id = a.Id,
                    Title = a.Title,
                    LastPricingDate = a.ItemPrices
                    .OrderByDescending(a=>a.Date)
                    .Select(a=>a.Date)
                    .FirstOrDefault(),
                })
                .ToListAsync();
            return Ok(items);
        }
    }
}
