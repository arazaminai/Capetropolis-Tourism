using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CapetropolisTourism.Models;
using Newtonsoft.Json;
using System.IO;

namespace CapetropolisTourism.Controllers
{
    public class HotelsController : Controller
    {
        private readonly ILogger<HotelsController> _logger;
        private List<HotelModel> hotels;
        private List<BookingModel> bookings;

        public HotelsController(ILogger<HotelsController> logger)
        {
            _logger = logger;

            // Initialize the hotels list
            using (StreamReader r = new StreamReader("DataStore/Hotels.json"))
            {
                string json = r.ReadToEnd();
                this.hotels = JsonConvert.DeserializeObject<List<HotelModel>>(json);
            }
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
            this.SaveBooking(booking);
            return View(booking);
        }

        private List<BookingModel> GetBookings()
        {
            // Initialize the bookings list
            using (StreamReader r = new StreamReader("DataStore/Bookings.json"))
            {
                string json = r.ReadToEnd();
                this.bookings = JsonConvert.DeserializeObject<List<BookingModel>>(json) ?? new List<BookingModel>();
                return this.bookings;
            }
        }

        private void SaveBooking(BookingModel booking)
        {
            this.GetBookings().Add(booking);
            string json = JsonConvert.SerializeObject(this.bookings);
            System.IO.File.WriteAllText("DataStore/Bookings.json", json);
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
