using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PulseAsset.Data;
using PulseAsset.Models;
using PulseAsset.Models.ViewModels;

namespace PulseAsset.Controllers;

[Authorize]
public class SettingsController : Controller
{
    private readonly ApplicationDbContext _context;

    public SettingsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Locations()
    {
        return View(_context.Locations.ToList());
    }
    
    [HttpGet]
    public IActionResult EditLocation(int id)
    {
        var location = _context.Locations.Find(id);
        
        return View(new LocationModel
        {
            Name = location.Name,
            Address = location.Address
        });
    }
    
    [HttpPost]
    public IActionResult EditLocation(LocationModel location)
    {
        // Check to ensure everything is filled out correctly
        if (ModelState.IsValid)
        {
            // Everything is good! Update the location in the database
            _context.Locations.Update(new LocationModel
            {
                LocationId = location.LocationId,
                Name = location.Name,
                Address = location.Address
            });
            
            _context.SaveChanges();
        }
        else
        {
            // Something wasn't filled out right. Send them back!
            return EditLocation(location.LocationId);
        }
        
        // Send them back to the list of locations after we finish the update
        return RedirectToAction("Locations", "Settings");
    }
    
    [HttpPost]
    public IActionResult Add(LocationViewModel location)
    {
        if (ModelState.IsValid)
        {
            _context.Locations.Add(new LocationModel
            {
                Name = location.Name,
                Address = location.Address
            });
            
            _context.SaveChanges();
        }
        
        return RedirectToAction("Locations", "Settings");
    }

    [HttpPost]
    public IActionResult DeleteLocation(int id)
    {
        // Look up the location by ID
        LocationModel location = _context.Locations.Find(id);
        
        // Confirm the location actually exists
        if (location != null)
        {
            // Location exists! Bye bye!
            _context.Locations.Remove(location);
            _context.SaveChanges();
        }
        
        // Send the user back to the location list view, regardless of whether deletion actually occurred
        return RedirectToAction("Locations", "Settings");
    }
}