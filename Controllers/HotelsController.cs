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
                // Print the JSON to the console
                // Console.WriteLine(json);
                foreach (var hotel in this.hotels)
                {
                    Console.WriteLine(hotel.name);
                }
            }
        }

        public IActionResult Index()
        {
            // Pass the list of hotels to the view
            return View(hotels);
        }

        public IActionResult Details(string name = "Hotels")
        {
            var hotel = this.hotels.FirstOrDefault(h => h.name.Equals(name, StringComparison.OrdinalIgnoreCase));
            // print the hotel name to the console
            Console.WriteLine(hotel.name);

            if (hotel != null)
            {
                string json = JsonConvert.SerializeObject(hotel);
                // Print the JSON to the console
                Console.WriteLine(json);
                return View(hotel);
            }
            
            return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
