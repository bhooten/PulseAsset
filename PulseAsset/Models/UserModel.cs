using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PulseAsset.Models;

public class UserModel : IdentityUser
{
    [Required(ErrorMessage = "You must provide a first name for the associate.")]
    [DataType(DataType.Text)]
    [StringLength(50, ErrorMessage = "The first name must be two or more characters and less than fifty.", MinimumLength = 2)]
    public string? FirstName { get; set; }
    
    [Required(ErrorMessage = "You must provide a last name for the associate.")]
    [DataType(DataType.Text)]
    [StringLength(50, ErrorMessage = "The last name must be two or more characters and less than fifty.", MinimumLength = 2)]
    public string? LastName { get; set; }
    
    [Required(ErrorMessage = "You must provide a role name for the associate.")]
    [DataType(DataType.Text)]
    public string? JobTitle { get; set; }
    
    [Required]
    public int? LocationId { get; set; }
    
    [ForeignKey("LocationId")]
    public LocationModel? Location { get; set; }
}