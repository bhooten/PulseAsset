using Microsoft.AspNetCore.Mvc;

namespace PulseAsset.Controllers;

public class AssetController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}