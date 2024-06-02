using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CapetropolisTourism.Models;
using CapetropolisTourism.DataStore;
using Newtonsoft.Json;

namespace CapetropolisTourism.Controllers;

public class MealsController : Controller
{
    private readonly ILogger<MealsController> _logger;
    private List<HotelModel> hotels = new Data<HotelModel>("Hotels").GetData();
    private Data<AgentModel> agents = new Data<AgentModel>("MealAgents");
    private Data<MealModel> meals = new Data<MealModel>("MealOrders");

    public MealsController(ILogger<MealsController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
    // return the list of agents and hotels to the view
        return View(new Tuple<List<AgentModel>, List<HotelModel>>(this.agents.GetData(), this.hotels));
    }

    public IActionResult MealSubmission(MealModel meal)
    {
        meal = this.meals.AddData(meal);
        Console.WriteLine(JsonConvert.SerializeObject(meal));
        return RedirectToAction("Index");
    }

    public IActionResult MealView(MealModel meal)
    {
        Console.WriteLine(JsonConvert.SerializeObject(meal));
        return View(meal);
    }

    public IActionResult Agents()
    {
        return View(this.agents.GetData());
    }

    public IActionResult AgentSubmission(AgentModel agent)
    {
        agent = this.agents.AddData(agent);
        Console.WriteLine(JsonConvert.SerializeObject(agent));
        return RedirectToAction("Agents");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
