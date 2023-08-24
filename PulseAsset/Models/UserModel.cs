using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PulseAsset.Models;

public class UserModel : IdentityUser
{
    [DataType(DataType.Text)]
    public string JobTitle { get; set; }
    
    [Required]
    public int LocationId { get; set; }
    
    [ForeignKey("LocationId")]
    public LocationModel Location { get; set; }
}