using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PulseAsset.Models;

public class CategoryModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CategoryId { get; set; }

    [Required]
    [Display(Name = "Category")]
    [DataType(DataType.Text)]
    public string Name;
    
    [Required]
    [DataType(DataType.MultilineText)]
    public string Description;
}