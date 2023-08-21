using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PulseAsset.Models;

public class LocationModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LocationId { get; set; }
    
    [Display(Description = "This is the friendly name for the location.")]
    [Required]
    [DataType(DataType.Text)]
    public string Name { get; set; }
    
    [Display(Description = "This is the street address for the location.")]
    [Required(ErrorMessage = "You must enter an address for this location.")]
    [DataType(DataType.Text)]
    public string Address { get; set; }
    
    
}