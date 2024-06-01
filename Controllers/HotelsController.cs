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

        public IActionResult BookingSubmission(string name, string roomType)
        {
            Console.WriteLine("BookingSubmission: " + name + " " + roomType);
            return RedirectToAction("Booking", new { name = name });
            // HotelModel hotel = hotels.FirstOrDefault(h => h.name.Equals(name, StringComparison.OrdinalIgnoreCase));

            // if (hotel != null)
            // {
            //     RoomModel selectedRoom = hotel.rooms.FirstOrDefault(r => r.name.Equals(roomType, StringComparison.OrdinalIgnoreCase));

            //     if (selectedRoom != null)
            //     {
            //         return View(new BookingModel
            //         {
            //             hotel = hotel,
            //             room = selectedRoom,
            //             meal = meal,
            //             date = date
            //         });
            //     }
            // }
            // return NotFound();
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

        public IActionResult Search(string query)
        {
            List<HotelModel> results = hotels.Where(h => h.name.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();

            return View(results);
        }

        public IActionResult Filter(string meal)
        {
            List<HotelModel> results = hotels.Where(h => h.meals.Contains(meal, StringComparer.OrdinalIgnoreCase)).ToList();

            return View(results);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
