using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HapusIsiDatabase.Models;

/// <summary>
/// File uploader model with custom primary key
/// </summary>
[Table("RealEstateUploaderFiles", Schema = "dbo")]
public class UploaderFile : TenantBaseEntity
{
    /// <summary>
    /// Custom primary key for UploaderFile - overrides Id from BaseEntity
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int FileUploadId { get; set; }

    /// <summary>
    /// Hide the inherited Id property since we use FileUploadId as primary key
    /// </summary>
    [NotMapped]
    public new int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string FileName { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string FilePath { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? FileType { get; set; }

    public long FileSize { get; set; }

    [MaxLength(50)]
    public string? FileExtension { get; set; }

    [MaxLength(255)]
    public string? OriginalFileName { get; set; }

    [MaxLength(100)]
    public string? ContentType { get; set; }

    [MaxLength(20)]
    public string? UploadStatus { get; set; }

    public string? Description { get; set; }

    public bool IsMainImage { get; set; } = false;

    public int? DisplayOrder { get; set; }

    [Column(TypeName = "datetime2")]
    public DateTime UploadDate { get; set; } = DateTime.UtcNow;

    // Foreign Keys
    public long? IklanId { get; set; }

    public int? RealEstateId { get; set; }

    [MaxLength(50)]
    public string? EntityType { get; set; }

    public int? EntityId { get; set; }

    // Navigation properties
    public virtual MasterIklan? Iklan { get; set; }
}