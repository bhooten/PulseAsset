using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PulseAsset.Data;
using PulseAsset.Models;

namespace PulseAsset.Controllers;

public class AssetController : Controller
{
    private readonly ApplicationDbContext _context;
    
    public AssetController(ApplicationDbContext context)
    {
        // Use DI to collect and assign the database context locally
        _context = context;
    }
    
    [HttpGet]
    public IActionResult Add()
    {
        // Collect available options for users, locations, and categories
        // to properly populate the drop-down lists on the form.
        ViewBag.Owners = _context.Users.Select(u => new SelectListItem
        {
            Text = u.Email,
            Value = u.Id
        }).ToList();
        
        ViewBag.Locations = _context.Locations.Select(l => new SelectListItem
        {
            Text = l.Name,
            Value = l.LocationId.ToString()
        }).ToList();

        ViewBag.Categories = _context.Categories.Select(c => new SelectListItem
        {
            Text = c.Name,
            Value = c.CategoryId.ToString()
        }).ToList();
    
        return View();
    }

    [HttpPost]
    public IActionResult Add(AssetModel asset)
    {
        // Check to confirm that form submission is valid and perform validation
        if (ModelState.IsValid)
        {
            // Everything looks good -- send it to the database!
            _context.Assets.Add(asset);
            _context.SaveChanges();
            
            // Send the user back to the asset list view
            return RedirectToAction("Index", "Asset");
        }
        else
        {
            // Something went wrong -- send the user back to the form to try again
            return Add();
        }
    }

    [HttpGet]
    public IActionResult Index()
    {
        // Just an asset list view -- get the assets and enumerate them
        return View(_context.Assets.ToList());
    }
    
    [HttpPost]
    public IActionResult Delete(int id)
    {
        // Look up the asset by ID
        AssetModel asset = _context.Assets.Find(id);
        
        // Confirm the asset actually exists
        if (asset != null)
        {
            // Asset exists! Bye bye!
            _context.Assets.Remove(asset);
            _context.SaveChanges();
        }
        
        // Send the user back to the asset list view, regardless of whether deletion actually occurred
        return RedirectToAction("Index", "Asset");
    }
    
    [HttpPost]
    public IActionResult Search()
    {
        // Get the search term from the form submission
        String searchTerm = Request.Form["query"].ToString().ToLower();
        
        // Search for the term in the assets table
        IEnumerable<AssetModel> assets = _context.Assets.Where(a => a.Name.ToLower().Contains(searchTerm) 
                                                || a.Description.ToLower().Contains(searchTerm) 
                                                || a.AssetId.Equals(searchTerm)
                                                || a.SerialNumber.ToLower().Contains(searchTerm));

        // Store the search query in the ViewBag for the view
        ViewBag.SearchQuery = Request.Form["query"].ToString();
        
        // Return the results to the view
        return View(assets.ToList());
    }
    
    [HttpGet]
    public IActionResult Edit(int id)
    {
        // Look up the asset by ID
        var asset = _context.Assets.Find(id);
        
        // Confirm the asset actually exists
        if (asset != null)
        {
            // Asset exists! Collect available options for users, locations, and categories
            // to properly populate the drop-down lists on the form.
            ViewBag.Owners = _context.Users.Select(u => new SelectListItem
            {
                Text = u.Email,
                Value = u.Id
            }).ToList();
        
            ViewBag.Locations = _context.Locations.Select(l => new SelectListItem
            {
                Text = l.Name,
                Value = l.LocationId.ToString()
            }).ToList();

            ViewBag.Categories = _context.Categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CategoryId.ToString()
            }).ToList();
            
            // Send it to the form for editing
            return View(asset);
        }
        else
        {
            // Asset doesn't exist! Send the user back to the asset list view
            return RedirectToAction("Index", "Asset");
        }
    }

    [HttpPost]
    public IActionResult Edit(AssetModel asset)
    {
        // Check to confirm that form submission is valid and perform validation
        if (ModelState.IsValid)
        {
            // Everything looks good -- send it to the database!
            _context.Assets.Update(asset);
            _context.SaveChanges();
            
            // Send the user back to the asset list view
            return RedirectToAction("Index", "Asset");
        }
        else
        {
            // Something went wrong -- send the user back to the form to try again
            return Edit(asset.AssetId);
        }
    }
}