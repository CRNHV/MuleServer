using BotServer.Lib.Data.Models;
using BotServer.Lib.Session.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BotServer.Controllers;


[Route("dashboard")]
public class DashboardController : Controller
{
    private readonly IBotSessionHolder _sessionHolder;

    public DashboardController(IBotSessionHolder sessionHolder)
    {
        _sessionHolder = sessionHolder;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<BotSession> sessions = _sessionHolder.GetSessions();
        return View(sessions);
    }
}
