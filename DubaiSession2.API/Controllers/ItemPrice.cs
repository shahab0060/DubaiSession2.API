using DubaiSession2.API.Context;
using DubaiSession2.API.Entities;
using DubaiSession2.API.ViewModels.Item;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DubaiSession2.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemPriceController : ControllerBase
    {
        #region constructor

        private readonly ComXContext _context;
        public ItemPriceController()
        {
            _context = new ComXContext();
        }

        #endregion

        [HttpGet("index/{itemId}")]
        public async Task<IActionResult> Index(long itemId)
        {
            List<ShowItemPriceBaseInformationViewModel> items = await _context
                .ItemPrices
                .Where(a => a.ItemId == itemId)
                .Include(a => a.CancellationPolicy)
                .Include(a => a.BookingDetails)
                .OrderByDescending(a => a.Date)
                .Select(a => new ShowItemPriceBaseInformationViewModel()
                {
                    Id = a.Id,
                    Date = a.Date,
                    CancellationPolicyTitle = a.CancellationPolicy.Name,
                    Price = a.Price,
                    Status = a.BookingDetails.Any() ? "locked" :
                    (_context.DimDates
                    .Where(d => d.Date == a.Date)
                    .Select(d => d.IsHoliday)
                    .FirstOrDefault() ? "holiday" : ""),
                })
                .ToListAsync();
            return Ok(items);
        }


        [HttpGet("edit/information/{id}")]
        public async Task<IActionResult> GetEditInformation(long id)
        {
            EditItemPriceInformation? item = await _context
                .ItemPrices
                .Where(a => a.Id == id)
                .Include(a => a.CancellationPolicy)
                .Include(a => a.BookingDetails)
                .OrderByDescending(a => a.Date)
                .Select(a => new EditItemPriceInformation()
                {
                    FromDate = a.Date,
                    ToDate = a.Date,
                    HolidayCancellationPolicyId = a.CancellationPolicyId,
                    HolidayPrice = a.Price,
                    ItemPriceId = a.Id,
                    NormalDayCancellationPolicyId = a.CancellationPolicyId,
                    NormalDayPrice = a.Price,
                    WeekendCancellationPolicyId = a.CancellationPolicyId,
                    WeekendPrice = a.Price
                })
                .FirstOrDefaultAsync();
            if (item is null) return NotFound();
            return Ok(item);
        }

        [HttpPost("create/itemPrice")]
        public async Task<IActionResult> Create([FromBody] CreateItemPriceInformation create)
        {
            if (!ModelState.IsValid) return BadRequest("please enter all the required fields valid");
            if (create.FromDate > create.ToDate) return BadRequest("start date can not be bigger than end date.");
            int totalDaysFromStartDateToCurrentDate = (create.FromDate - DateTime.Now).Days;
            int totalDaysFromEndDateToCurrentDate = (create.ToDate - DateTime.Now).Days;
            if (totalDaysFromStartDateToCurrentDate < 1) return BadRequest("from can start from tommorrow");
            if (totalDaysFromStartDateToCurrentDate > 90) return BadRequest("from date cannot be available for reservation 90 days after the current date");
            if (totalDaysFromEndDateToCurrentDate > 90) return BadRequest("to date cannot be available for reservation 90 days after the current date");
            int totalDays = (int)(create.ToDate - create.FromDate).TotalDays;
            totalDays++;
            for (int i = 0; i <= totalDays; i++)
            {
                int negativeI = i * -1;
                DateTime time = create.ToDate.AddDays(negativeI);
                if (!await _context
                    .ItemPrices.AnyAsync(a => a.ItemId == create.ItemId && a.Date == time))
                {
                    bool isHoliday = await _context.DimDates
                    .AnyAsync(a => a.Date == time && a.IsHoliday);
                    bool isWeekend = time.Date.DayOfWeek == DayOfWeek.Sunday || time.Date.DayOfWeek == DayOfWeek.Monday;
                    decimal price = 0;
                    long cancellationPolicyId = 0;
                    if (isWeekend)
                    {
                        price = create.WeekendPrice ?? 0;
                        cancellationPolicyId = create.WeekendCancellationPolicyId ?? 0;
                    }
                    if (isHoliday)
                    {
                        price = create.HolidayPrice ?? 0;
                        cancellationPolicyId = create.HolidayCancellationPolicyId ?? 0;
                    }
                    if (price == 0)
                        price = create.NormalDayPrice;
                    if (cancellationPolicyId == 0)
                        cancellationPolicyId = create.NormalDayCancellationPolicyId;
                    ItemPrice itemPrice = new ItemPrice()
                    {
                        ItemId = create.ItemId,
                        Price = price,
                        CancellationPolicyId = cancellationPolicyId,
                        Date = time,
                        Guid = Guid.NewGuid(),
                    };
                    await _context.ItemPrices.AddAsync(itemPrice);
                }
                await _context.SaveChangesAsync();
            }
            return Ok("your operation append successfully.");
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] long id)
        {
            ItemPrice? itemPrice = await _context.ItemPrices
                .Include(a => a.BookingDetails)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (itemPrice == null) return BadRequest("item price not found");
            if (itemPrice.BookingDetails.Any())
                return BadRequest("you cannot delete this night becuase it has been reserved by a user previously");
            _context
               .ItemPrices.Remove(itemPrice);
            await _context.SaveChangesAsync ();
            return Ok("night removed successfully");
        }
    }
}
