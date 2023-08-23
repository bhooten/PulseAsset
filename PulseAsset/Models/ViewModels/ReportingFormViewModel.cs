using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PulseAsset.Models.ViewModels;

public class ReportingFormViewModel
{
    [Display(Name = "Minimum Price")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(12, 2)")]
    public decimal? LowerPrice { get; set; }
    
    [Display(Name = "Maximum Price")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(12, 2)")]
    public decimal? UpperPrice { get; set; }
    
    [Required(ErrorMessage = "You must select a valid category for the asset.")]
    public int? CategoryId { get; set; }
    
    [ForeignKey("CategoryId")]
    public CategoryModel? Category { get; set; }
    
    [Required(ErrorMessage = "You must select a valid location for the asset.")]
    public int? LocationId { get; set; }
    
    [ForeignKey("LocationId")]
    public LocationModel? Location { get; set; }
    
    public string? UserId { get; set; }
    
    [ForeignKey("UserId")]
    public UserModel? Owner { get; set; }
}