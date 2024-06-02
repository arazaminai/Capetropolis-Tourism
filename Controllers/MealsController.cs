﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CapetropolisTourism.Models;

namespace CapetropolisTourism.Controllers;

public class MealsController : Controller
{
    private readonly ILogger<MealsController> _logger;

    public MealsController(ILogger<MealsController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Agents()
    {
        return View();
    }

    public IActionResult AgentSubmission(AgentModel agent)
    {
        return View(agent);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
