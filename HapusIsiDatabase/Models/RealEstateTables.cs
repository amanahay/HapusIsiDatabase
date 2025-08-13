using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HapusIsiDatabase.Models;

/// <summary>
/// Real Estate Category model
/// </summary>
[Table("RealEstateCategory", Schema = "dbo")]
public class RealEstateCategory : TenantBaseEntity
{
    [Required]
    [MaxLength(100)]
    public string CategoryName { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? CategoryDescription { get; set; }

    [MaxLength(50)]
    public string? CategoryCode { get; set; }

    public bool IsActive { get; set; } = true;

    public int? ParentCategoryId { get; set; }

    public int SortOrder { get; set; }

    [MaxLength(255)]
    public string? CategoryIcon { get; set; }

    [MaxLength(7)]
    public string? CategoryColor { get; set; }

    // Navigation properties
    public virtual RealEstateCategory? ParentCategory { get; set; }
    public virtual ICollection<RealEstateCategory> SubCategories { get; set; } = new List<RealEstateCategory>();
    public virtual ICollection<RealEstateSubCategory> RealEstateSubCategories { get; set; } = new List<RealEstateSubCategory>();
    public virtual ICollection<MasterIklan> MasterIklans { get; set; } = new List<MasterIklan>();
}

/// <summary>
/// Real Estate Sub Category model
/// </summary>
[Table("RealEstateSubCategory", Schema = "dbo")]
public class RealEstateSubCategory : TenantBaseEntity
{
    [Required]
    [MaxLength(100)]
    public string SubCategoryName { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? SubCategoryDescription { get; set; }

    [MaxLength(50)]
    public string? SubCategoryCode { get; set; }

    public bool IsActive { get; set; } = true;

    public int CategoryId { get; set; }

    public int SortOrder { get; set; }

    [MaxLength(255)]
    public string? SubCategoryIcon { get; set; }

    [MaxLength(7)]
    public string? SubCategoryColor { get; set; }

    // Navigation properties
    public virtual RealEstateCategory Category { get; set; } = null!;
    public virtual ICollection<MasterIklan> MasterIklans { get; set; } = new List<MasterIklan>();
}

/// <summary>
/// Real Estate Owner model
/// </summary>
[Table("RealEstateOwner", Schema = "dbo")]
public class RealEstateOwner : TenantBaseEntity
{
    [Required]
    [MaxLength(100)]
    public string OwnerName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? OwnerEmail { get; set; }

    [MaxLength(20)]
    public string? OwnerPhone { get; set; }

    [MaxLength(255)]
    public string? OwnerAddress { get; set; }

    [MaxLength(50)]
    public string? OwnerIdNumber { get; set; }

    [MaxLength(20)]
    public string? OwnerIdType { get; set; }

    [Column(TypeName = "datetime2")]
    public DateTime? OwnerBirthDate { get; set; }

    [MaxLength(10)]
    public string? OwnerGender { get; set; }

    [MaxLength(50)]
    public string? OwnerOccupation { get; set; }

    [MaxLength(100)]
    public string? OwnerCompany { get; set; }

    public bool IsActive { get; set; } = true;

    [MaxLength(255)]
    public string? Notes { get; set; }

    // Navigation properties
    public virtual ICollection<RealEstate> RealEstates { get; set; } = new List<RealEstate>();
}

/// <summary>
/// Real Estate Price Range model
/// </summary>
[Table("RealEstatePriceRange", Schema = "dbo")]
public class RealEstatePriceRange : TenantBaseEntity
{
    [Required]
    [MaxLength(100)]
    public string PriceRangeName { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal MinPrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal MaxPrice { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;

    public int SortOrder { get; set; }

    [MaxLength(50)]
    public string? PriceRangeCode { get; set; }

    [MaxLength(10)]
    public string? Currency { get; set; } = "IDR";

    // Navigation properties
    public virtual ICollection<RealEstate> RealEstates { get; set; } = new List<RealEstate>();
}

/// <summary>
/// Real Estate Location model
/// </summary>
[Table("RealEstateLocation", Schema = "dbo")]
public class RealEstateLocation : TenantBaseEntity
{
    [Required]
    [MaxLength(100)]
    public string LocationName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? LocationCode { get; set; }

    [MaxLength(50)]
    public string? LocationType { get; set; }

    public int? ParentLocationId { get; set; }

    [MaxLength(255)]
    public string? FullAddress { get; set; }

    [MaxLength(10)]
    public string? PostalCode { get; set; }

    [Column(TypeName = "decimal(10,6)")]
    public decimal? Latitude { get; set; }

    [Column(TypeName = "decimal(10,6)")]
    public decimal? Longitude { get; set; }

    public bool IsActive { get; set; } = true;

    public int SortOrder { get; set; }

    [MaxLength(50)]
    public string? Province { get; set; }

    [MaxLength(50)]
    public string? City { get; set; }

    [MaxLength(50)]
    public string? District { get; set; }

    [MaxLength(50)]
    public string? Village { get; set; }

    // Navigation properties
    public virtual RealEstateLocation? ParentLocation { get; set; }
    public virtual ICollection<RealEstateLocation> SubLocations { get; set; } = new List<RealEstateLocation>();
    public virtual ICollection<RealEstate> RealEstates { get; set; } = new List<RealEstate>();
}

/// <summary>
/// Real Estate Amenity model
/// </summary>
[Table("RealEstateAmenity", Schema = "dbo")]
public class RealEstateAmenity : TenantBaseEntity
{
    [Required]
    [MaxLength(100)]
    public string AmenityName { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? AmenityDescription { get; set; }

    [MaxLength(50)]
    public string? AmenityCode { get; set; }

    [MaxLength(50)]
    public string? AmenityCategory { get; set; }

    [MaxLength(255)]
    public string? AmenityIcon { get; set; }

    public bool IsActive { get; set; } = true;

    public int SortOrder { get; set; }

    [MaxLength(255)]
    public string? Notes { get; set; }

    // Navigation properties
    public virtual ICollection<RealEstateAmenityMapping> RealEstateAmenityMappings { get; set; } = new List<RealEstateAmenityMapping>();
}

/// <summary>
/// Real Estate main model
/// </summary>
[Table("RealEstate", Schema = "dbo")]
public class RealEstate : TenantBaseEntity
{
    [Required]
    [MaxLength(100)]
    public string PropertyTitle { get; set; } = string.Empty;

    public string? PropertyDescription { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PropertyPrice { get; set; }

    [MaxLength(50)]
    public string? PropertyStatus { get; set; }

    [MaxLength(20)]
    public string? PropertyType { get; set; }

    public int CategoryId { get; set; }

    public int? SubCategoryId { get; set; }

    public int? OwnerId { get; set; }

    public int? LocationId { get; set; }

    public int? PriceRangeId { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? LandSize { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? BuildingSize { get; set; }

    public int? Bedrooms { get; set; }

    public int? Bathrooms { get; set; }

    public int? Floors { get; set; }

    [MaxLength(50)]
    public string? CertificateType { get; set; }

    public int? YearBuilt { get; set; }

    [MaxLength(20)]
    public string? PropertyCondition { get; set; }

    [MaxLength(50)]
    public string? PropertyOrientation { get; set; }

    [MaxLength(100)]
    public string? RoadAccess { get; set; }

    public int? ElectricPower { get; set; }

    [MaxLength(50)]
    public string? WaterSource { get; set; }

    [Column(TypeName = "decimal(10,6)")]
    public decimal? Latitude { get; set; }

    [Column(TypeName = "decimal(10,6)")]
    public decimal? Longitude { get; set; }

    [MaxLength(255)]
    public string? MainImage { get; set; }

    public bool IsActive { get; set; } = true;

    public bool IsFeatured { get; set; } = false;

    [Column(TypeName = "datetime2")]
    public DateTime? ListingDate { get; set; }

    [Column(TypeName = "datetime2")]
    public DateTime? ExpiryDate { get; set; }

    public int ViewCount { get; set; } = 0;

    public int ContactCount { get; set; } = 0;

    [MaxLength(255)]
    public string? AdditionalNotes { get; set; }

    // Navigation properties
    public virtual RealEstateCategory Category { get; set; } = null!;
    public virtual RealEstateSubCategory? SubCategory { get; set; }
    public virtual RealEstateOwner? Owner { get; set; }
    public virtual RealEstateLocation? Location { get; set; }
    public virtual RealEstatePriceRange? PriceRange { get; set; }
    public virtual ICollection<RealEstateAmenityMapping> RealEstateAmenityMappings { get; set; } = new List<RealEstateAmenityMapping>();
}

/// <summary>
/// Real Estate Amenity Mapping model (many-to-many relationship)
/// </summary>
[Table("RealEstateAmenityMapping", Schema = "dbo")]
public class RealEstateAmenityMapping : TenantBaseEntity
{
    public int RealEstateId { get; set; }

    public int AmenityId { get; set; }

    [MaxLength(255)]
    public string? Notes { get; set; }

    public bool IsAvailable { get; set; } = true;

    [MaxLength(50)]
    public string? AmenityValue { get; set; }

    // Navigation properties
    public virtual RealEstate RealEstate { get; set; } = null!;
    public virtual RealEstateAmenity Amenity { get; set; } = null!;
}