using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PulseAsset.Data;
using PulseAsset.Models;

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
    public IActionResult Locations()
    {
        // We're just getting a list of the locations
        return View(_context.Locations.ToList());
    }
    
    [HttpGet]
    public IActionResult EditLocation(int id)
    {
        LocationModel location = _context.Locations.Find(id);

        if (location == null)
        {
            return RedirectToAction("Locations", "Settings");
        }
        
        return View(location);
    }
    
    [HttpPost]
    public IActionResult EditLocation(LocationModel location)
    {
        // Check to ensure everything is filled out correctly
        if (ModelState.IsValid)
        {
            // Everything is good! Update the location in the database
            _context.Locations.Update(location);
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
    public IActionResult AddLocation(LocationModel location)
    {
        if (ModelState.IsValid)
        {
            _context.Locations.Add(location);
            _context.SaveChanges();
        }
        else
        {
            return AddLocation();
        }
        
        return RedirectToAction("Locations", "Settings");
    }

    [HttpGet]
    public IActionResult AddLocation()
    {
        return View();
    }

    [HttpPost]
    public IActionResult DeleteLocation(int id)
    {
        // Look up the location by ID
        LocationModel location = _context.Locations.Find(id);
        
        // Confirm the location actually exists
        if (location != null)
        {
            // Location exists! Let's first make sure it's not the only one,
            // as well as no assets are assigned to it.
            // 
            // If logic doesn't pass, we'll just skip deletion and return to the view
            if (_context.Categories.Count() > 1)
            {
                // There are multiple! Good to remove.
                if (_context.Assets.Where(a => a.LocationId == id).Count() == 0)
                {
                    _context.Locations.Remove(location);
                    _context.SaveChanges();
                }
                else
                {
                    ViewBag.ErrorType = "danger";
                    ViewBag.ErrorMessage = "You can not delete a location that has assets assigned to it.";

                    return View("Locations", _context.Locations.ToList());
                }
            }
            else
            {
                ViewBag.ErrorType = "danger";
                ViewBag.ErrorMessage = "You must create a new location before you can delete the requested one.";

                return View("Locations", _context.Locations.ToList());
            }
        }
        else
        {
            ViewBag.ErrorType = "warning";
            ViewBag.ErrorMessage = "The location deletion was attempted on does not exist.";
            
            return View("Locations", _context.Locations.ToList());
        }

        // Send the user back to the location list view, regardless of whether deletion actually occurred
        return RedirectToAction("Locations", "Settings");
    }
    
    [HttpGet]
    public IActionResult Categories()
    {
        // We're just getting a list of the categories
        return View(_context.Categories.ToList());
    }

    [HttpGet]
    public IActionResult EditCategory(int id)
    {
        CategoryModel category = _context.Categories.Find(id);

        if (category == null)
        {
            return RedirectToAction("Categories", "Settings");
        }
        
        return View(category);
    }

    [HttpPost]
    public IActionResult EditCategory(CategoryModel category)
    {
        // Check to ensure everything is filled out correctly
        if (ModelState.IsValid)
        {
            // Everything is good! Update the location in the database
            _context.Categories.Update(category);
            _context.SaveChanges();
        }
        else
        {
            // Something wasn't filled out right. Send them back!
            return EditCategory(category.CategoryId);
        }
        
        // Send them back to the list of locations after we finish the update
        return RedirectToAction("Categories", "Settings");
    }

    [HttpPost]
    public IActionResult AddCategory(CategoryModel category)
    {
        if (ModelState.IsValid)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        else
        {
            return AddLocation();
        }
        
        return RedirectToAction("Categories", "Settings");
    }

    [HttpGet]
    public IActionResult AddCategory()
    {
        return View();
    }

    [HttpPost]
    public IActionResult DeleteCategory(int id)
    {
        // Look up the Category by ID
        CategoryModel category = _context.Categories.Find(id);
        
        // Confirm the category actually exists
        if (category != null)
        {
            // Category exists! Let's first make sure it's not the only one,
            // as well as no assets are assigned to it.
            // 
            // If logic doesn't pass, we'll just skip deletion and return to the view
            if (_context.Categories.Count() > 1)
            {
                // There are multiple! Good to remove.
                if (_context.Assets.Where(a => a.CategoryId == id).Count() == 0)
                {
                    _context.Categories.Remove(category);
                    _context.SaveChanges();
                }
                else
                {
                    ViewBag.ErrorType = "danger";
                    ViewBag.ErrorMessage = "You can not delete a category that has assets assigned to it.";

                    return View("Categories", _context.Categories.ToList());
                }
            }
            else
            {
                ViewBag.ErrorType = "danger";
                ViewBag.ErrorMessage = "You must create a new category before you can delete the requested one.";

                return View("Categories", _context.Categories.ToList());
            }
        }
        else
        {
            ViewBag.ErrorType = "warning";
            ViewBag.ErrorMessage = "The category deletion was attempted on does not exist.";
            
            return View("Categories", _context.Categories.ToList());
        }
        
        // Send the user back to the category list view, regardless of whether deletion actually occurred
        return RedirectToAction("Categories", "Settings");
    }
}