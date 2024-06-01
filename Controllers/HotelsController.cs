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
                    // Console.WriteLine(hotel.name);
                }
            }
        }

        public IActionResult Index()
        {
            // Pass the list of hotels to the view
            return View(this.hotels);
        }

        public ActionResult Details(string name)
        {
            foreach (var hotel in this.hotels)
            {
                if (hotel.name == name)
                {
                    return View(hotel);
                }
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
