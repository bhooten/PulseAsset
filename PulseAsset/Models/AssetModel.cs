using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PulseAsset.Models;

public class AssetModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AssetId { get; set; }
    
    [Required(ErrorMessage = "You must supply a name for the asset.")]
    [DataType(DataType.Text)]
    public string Name { get; set; }
    
    [DataType(DataType.MultilineText)]
    public string Description { get; set; }
    
    [Display(Name = "Serial Number")]
    [StringLength(50, ErrorMessage = "The serial number of the asset can not exceed fifty characters.")]
    public string SerialNumber { get; set; }
    
    [Display(Name = "Purchase Price")]
    [Required(ErrorMessage = "You must supply a purchase price for the asset.")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(12, 2)")]
    public decimal Price { get; set; }

    [Display(Name = "Date of Purchase")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? PurchaseDate { get; set; }
    
    [Display(Name = "Date of Disposal")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? DisposalDate { get; set; }
    
    [Required(ErrorMessage = "You must select a valid category for the asset.")]
    public int CategoryId { get; set; }
    
    [ForeignKey("CategoryId")]
    public CategoryModel? Category { get; set; }
    
    [Required(ErrorMessage = "You must select a valid location for the asset.")]
    public int LocationId { get; set; }
    
    [ForeignKey("LocationId")]
    public LocationModel? Location { get; set; }
    
    public string? UserId { get; set; }
    
    [ForeignKey("UserId")]
    public UserModel? Owner { get; set; }
}