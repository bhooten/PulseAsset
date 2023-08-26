using PulseAsset.Controllers;
using PulseAsset.Models;
using PulseAsset.Models.ViewModels;

namespace PulseAsset.Tests.Controllers;

[TestClass]
public class ReportingControllerTest
{
    private static List<AssetModel> seedData = new();

    public ReportingControllerTest()
    {
        seedData.Add(new AssetModel()
        {
            AssetId = 0,
            CategoryId = 1,
            Description = "This is an iPad Pro.",
            LocationId = 1,
            Name = "Apple iPad Pro",
            Price = (decimal) 899.95,
        });
        
        seedData.Add(new AssetModel()
        {
            AssetId = 1,
            CategoryId = 3,
            Description = "This is a MacBook Pro.",
            LocationId = 1,
            Name = "Apple MacBook Pro",
            Price = (decimal) 2000.95,
            UserId = "5"
        });
        
        seedData.Add(new AssetModel()
        {
            AssetId = 2,
            CategoryId = 2,
            Description = "This is a Microsoft Surface Pro 8.",
            LocationId = 2,
            Name = "Microsoft Surface Pro 8",
            Price = (decimal) 1499.95
        });
    }
    
    [TestMethod]
    public void testReportFilteringCategory()
    {
        // Model only contains a directive to filter by CategoryId
        ReportingFormViewModel model = new ReportingFormViewModel()
        {
            CategoryId = 2
        };
        
        // Call the business logic function against the seed data and model
        List<AssetModel> results = ReportingController.FilterReportData(seedData, model);
        
        // Confirm that the expected result matches the actual result
        Assert.AreEqual(seedData.Where(a => a.CategoryId == 2).Count(), results.Count);
    }
    
    [TestMethod]
    public void testReportFilteringLocation()
    {
        // Model only contains a directive to filter by LocationId
        ReportingFormViewModel model = new ReportingFormViewModel()
        {
            LocationId = 1
        };
        
        // Call the business logic function against the seed data and model
        List<AssetModel> results = ReportingController.FilterReportData(seedData, model);
        
        // Confirm that the expected result matches the actual result
        Assert.AreEqual(seedData.Where(a => a.LocationId == 1).Count(), results.Count);
    }
    
    [TestMethod]
    public void testReportFilteringLocationAndCategory()
    {
        // Model only contains a directive to filter by LocationId and CategoryId
        ReportingFormViewModel model = new ReportingFormViewModel()
        {
            LocationId = 1,
            CategoryId = 3
        };
        
        // Call the business logic function against the seed data and model
        List<AssetModel> results = ReportingController.FilterReportData(seedData, model);
        
        // Confirm that the expected result matches the actual result
        Assert.AreEqual(seedData.Where(a => a.LocationId == 1 && a.CategoryId == 3).Count(), results.Count);
    }
    
    [TestMethod]
    public void testReportFilteringAllCriteria()
    {
        // Model only contains a directive to filter by LocationId and CategoryId
        ReportingFormViewModel model = new ReportingFormViewModel()
        {
            LocationId = 1,
            CategoryId = 3,
            LowerPrice = (decimal) 2000.00,
            UpperPrice = (decimal) 3000.00,
            UserId = "5"
        };
        
        // Call the business logic function against the seed data and model
        List<AssetModel> results = ReportingController.FilterReportData(seedData, model);
        
        // Confirm that the expected result matches the actual result
        Assert.AreEqual(seedData.Where(a => a.LocationId == 1 && a.CategoryId == 3 
                                                              && a.Price <= 3000 && a.Price >= 2000 
                                                              && a.UserId == "5").Count(), results.Count);
    }
}