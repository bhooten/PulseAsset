using System.ComponentModel.DataAnnotations;

namespace PulseAsset.Models.ViewModels;

public class LocationViewModel
{
    [Display(Description = "This is the friendly name for the location.")]
    [Required]
    [DataType(DataType.Text)]
    public string Name { get; set; }
    
    [Display(Description = "This is the street address for the location.")]
    [Required(ErrorMessage = "You must enter an address for this location.")]
    [DataType(DataType.Text)]
    public string Address { get; set; }
}