using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CapetropolisTourism.Models;
using CapetropolisTourism.DataStore;
using Newtonsoft.Json;
using System.IO;

namespace CapetropolisTourism.Controllers
{
    public class HotelsController : Controller
    {
        private readonly ILogger<HotelsController> _logger;
        private List<HotelModel> hotels;
        private Data<BookingModel> bookings;

        public HotelsController(ILogger<HotelsController> logger)
        {
            _logger = logger;

            this.hotels = new Data<HotelModel>("Hotels").GetData();
            this.bookings = new Data<BookingModel>("Bookings");
        }

        public IActionResult Index()
        {
            // Pass the list of hotels to the view
            return View(this.hotels);
        }

        public IActionResult Booking(string name)
        {
            HotelModel hotel = hotels.FirstOrDefault(h => h.name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (hotel != null)
            {
                return View(hotel);
            }
            return NotFound();
        }

        public IActionResult BookingSubmission(BookingModel booking)
        {
            var bookingData = JsonConvert.SerializeObject(booking);
            Console.WriteLine(bookingData);
            booking = this.bookings.AddData(booking);
            return View(booking);
        }

        public JsonResult Details(string name)
        {
            HotelModel hotel = hotels.FirstOrDefault(h => h.name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (hotel != null)
            {
                return Json(hotel);
            }
            return Json(null);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
