using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HapusIsiDatabase.Models;

/// <summary>
/// Master Iklan (Advertisement Master) model with custom primary key
/// </summary>
[Table("RealEstateMaster_Iklan", Schema = "dbo")]
public class MasterIklan : TenantBaseEntity
{
    /// <summary>
    /// Custom primary key for MasterIklan - overrides Id from BaseEntity
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long IdIklan { get; set; }

    /// <summary>
    /// Hide the inherited Id property since we use IdIklan as primary key
    /// </summary>
    [NotMapped]
    public new int Id { get; set; }

    [MaxLength(100)]
    public string? JudulIklan { get; set; }

    public string? DeskripsiIklan { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? HargaIklan { get; set; }

    [MaxLength(50)]
    public string? StatusIklan { get; set; }

    [MaxLength(20)]
    public string? TipeIklan { get; set; }

    public int? KategoriId { get; set; }

    public int? SubKategoriId { get; set; }

    [MaxLength(100)]
    public string? LokasiIklan { get; set; }

    [Column(TypeName = "datetime2")]
    public DateTime? TanggalMulaiIklan { get; set; }

    [Column(TypeName = "datetime2")]
    public DateTime? TanggalBerakhirIklan { get; set; }

    public bool IsActive { get; set; } = true;

    [MaxLength(255)]
    public string? GambarUtama { get; set; }

    public int? JumlahKamar { get; set; }

    public int? JumlahKamarMandi { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? LuasTanah { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? LuasBangunan { get; set; }

    [MaxLength(50)]
    public string? SertifikatTipe { get; set; }

    public int? TahunBangun { get; set; }

    [MaxLength(20)]
    public string? KondisiProperti { get; set; }

    [MaxLength(50)]
    public string? OrientasiProperti { get; set; }

    [MaxLength(100)]
    public string? AksesJalan { get; set; }

    public int? DayaListrik { get; set; }

    [MaxLength(50)]
    public string? SumberAir { get; set; }

    [MaxLength(255)]
    public string? FasilitasUmum { get; set; }

    [MaxLength(255)]
    public string? FasilitasKeamanan { get; set; }

    [Column(TypeName = "decimal(10,6)")]
    public decimal? Latitude { get; set; }

    [Column(TypeName = "decimal(10,6)")]
    public decimal? Longitude { get; set; }

    [MaxLength(100)]
    public string? NamaAgen { get; set; }

    [MaxLength(20)]
    public string? TeleponAgen { get; set; }

    [MaxLength(100)]
    public string? EmailAgen { get; set; }

    [MaxLength(255)]
    public string? AlamatLengkap { get; set; }

    [MaxLength(10)]
    public string? KodePos { get; set; }

    // Navigation properties
    public virtual RealEstateCategory? Kategori { get; set; }
    public virtual RealEstateSubCategory? SubKategori { get; set; }
    public virtual ICollection<UploaderFile> Files { get; set; } = new List<UploaderFile>();
}