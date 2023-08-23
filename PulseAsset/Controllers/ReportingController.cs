using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PulseAsset.Data;
using PulseAsset.Models;
using PulseAsset.Models.ViewModels;

namespace PulseAsset.Controllers;

public class ReportingController : Controller
{
    private readonly ApplicationDbContext _context;
    
    public ReportingController(ApplicationDbContext context)
    {
        // Use DI to collect and assign the database context locally
        _context = context;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        // Collect available options for users, locations, and categories
        // to properly populate the drop-down lists on the form.
        ViewBag.Owners = _context.Users.Select(u => new SelectListItem
        {
            Text = u.FirstName + " " + u.LastName,
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
    public IActionResult Generate(ReportingFormViewModel reportRequest)
    {
        // First... we need to narrow down the list of assets to only the ones that match the criteria
        // specified by the user in the form. We can do this by supplying the report request to #FilterReportData
        IEnumerable<AssetModel> assets = FilterReportData(reportRequest);
        
        // Since a CSV is just plaintext with commas separating the values, we can use a StringBuilder
        StringBuilder csvBuilder = new StringBuilder();

        // Add the header row to the CSV
        csvBuilder.AppendLine("Asset ID,Asset Name,Asset Serial Number,Asset Description,Asset Owner,Asset Location," +
                              "Asset Category,Asset Purchase Date,Asset Disposal Date,Asset Purchase Price");

        // Iterate through all of the assets in the applicable data
        foreach (AssetModel asset in assets)
        {
            // Add a new line to the CSV with the asset's data
            csvBuilder.AppendLine($"{asset.AssetId},{asset.Name},{asset.SerialNumber},{asset.Description}," +
                                  $"{asset.Owner?.FirstName ?? ""} {asset.Owner?.LastName ?? ""},{asset.Location?.Name}," +
                                  $"{asset.Category?.Name},{asset.PurchaseDate?.ToString() ?? ""},{asset.DisposalDate?.ToString() ?? ""}," +
                                  $"{asset.Price}");
        }
        
        // Convert the CSV to a byte array and then to a memory stream
        var byteArray = Encoding.UTF8.GetBytes(csvBuilder.ToString());
        var stream = new MemoryStream(byteArray);

        // Return the CSV file
        return File(stream, "text/csv", "report.csv");
    }

    private IEnumerable<AssetModel> FilterReportData(ReportingFormViewModel model)
    {
        List<AssetModel> assets = _context.Assets.ToList();
        
        // If the Category is set, filter out non-matching results
        if(model.CategoryId != null)
            assets = assets.Where(a => a.CategoryId == model.CategoryId).ToList();
        
        // If the Location is set, filter out non-matching results
        if(model.LocationId != null)
            assets = assets.Where(a => a.LocationId == model.LocationId).ToList();
        
        // If the Owner is set, filter out non-matching results
        if(model.UserId != null)
            assets = assets.Where(a => a.UserId == model.UserId).ToList();
        
        // If the LowerPrice is set, filter out non-matching results
        if(model.LowerPrice != null)
            assets = assets.Where(a => a.Price >= model.LowerPrice).ToList();
        
        // If the UpperPrice is set, filter out non-matching results
        if(model.UpperPrice != null)
            assets = assets.Where(a => a.Price <= model.UpperPrice).ToList();
        
        // Filtering done! Let's hand the data set back.
        return assets;
    }
}