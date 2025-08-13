using Microsoft.EntityFrameworkCore;
using HapusIsiDatabase.Models;

namespace HapusIsiDatabase.Data;

/// <summary>
/// Application Database Context with proper multi-tenant configurations
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // DbSets
    public DbSet<MasterIklan> MasterIklans { get; set; }
    public DbSet<UploaderFile> UploaderFiles { get; set; }
    public DbSet<RealEstateCategory> RealEstateCategories { get; set; }
    public DbSet<RealEstateSubCategory> RealEstateSubCategories { get; set; }
    public DbSet<RealEstateOwner> RealEstateOwners { get; set; }
    public DbSet<RealEstatePriceRange> RealEstatePriceRanges { get; set; }
    public DbSet<RealEstateLocation> RealEstateLocations { get; set; }
    public DbSet<RealEstateAmenity> RealEstateAmenities { get; set; }
    public DbSet<RealEstate> RealEstates { get; set; }
    public DbSet<RealEstateAmenityMapping> RealEstateAmenityMappings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure MasterIklan
        ConfigureMasterIklan(modelBuilder);
        
        // Configure UploaderFile
        ConfigureUploaderFile(modelBuilder);
        
        // Configure RealEstate entities
        ConfigureRealEstateEntities(modelBuilder);
        
        // Configure global query filters for multi-tenancy
        ConfigureGlobalQueryFilters(modelBuilder);
        
        // Configure indexes
        ConfigureIndexes(modelBuilder);
    }

    private void ConfigureMasterIklan(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<MasterIklan>();
        
        // Configure primary key
        entity.HasKey(e => e.IdIklan);
        
        // Configure table
        entity.ToTable("RealEstateMaster_Iklan", "dbo");
        
        // Configure properties
        entity.Property(e => e.IdIklan)
            .HasColumnName("IdIklan")
            .ValueGeneratedOnAdd();
            
        entity.Property(e => e.JudulIklan)
            .HasMaxLength(100);
            
        entity.Property(e => e.HargaIklan)
            .HasColumnType("decimal(18,2)");
            
        entity.Property(e => e.StatusIklan)
            .HasMaxLength(50);
            
        entity.Property(e => e.TipeIklan)
            .HasMaxLength(20);
            
        entity.Property(e => e.LokasiIklan)
            .HasMaxLength(100);
            
        entity.Property(e => e.TanggalMulaiIklan)
            .HasColumnType("datetime2");
            
        entity.Property(e => e.TanggalBerakhirIklan)
            .HasColumnType("datetime2");
            
        entity.Property(e => e.GambarUtama)
            .HasMaxLength(255);
            
        entity.Property(e => e.LuasTanah)
            .HasColumnType("decimal(10,2)");
            
        entity.Property(e => e.LuasBangunan)
            .HasColumnType("decimal(10,2)");
            
        entity.Property(e => e.SertifikatTipe)
            .HasMaxLength(50);
            
        entity.Property(e => e.KondisiProperti)
            .HasMaxLength(20);
            
        entity.Property(e => e.OrientasiProperti)
            .HasMaxLength(50);
            
        entity.Property(e => e.AksesJalan)
            .HasMaxLength(100);
            
        entity.Property(e => e.SumberAir)
            .HasMaxLength(50);
            
        entity.Property(e => e.FasilitasUmum)
            .HasMaxLength(255);
            
        entity.Property(e => e.FasilitasKeamanan)
            .HasMaxLength(255);
            
        entity.Property(e => e.Latitude)
            .HasColumnType("decimal(10,6)");
            
        entity.Property(e => e.Longitude)
            .HasColumnType("decimal(10,6)");
            
        entity.Property(e => e.NamaAgen)
            .HasMaxLength(100);
            
        entity.Property(e => e.TeleponAgen)
            .HasMaxLength(20);
            
        entity.Property(e => e.EmailAgen)
            .HasMaxLength(100);
            
        entity.Property(e => e.AlamatLengkap)
            .HasMaxLength(255);
            
        entity.Property(e => e.KodePos)
            .HasMaxLength(10);

        // Configure relationships
        entity.HasOne(e => e.Kategori)
            .WithMany(c => c.MasterIklans)
            .HasForeignKey(e => e.KategoriId)
            .OnDelete(DeleteBehavior.SetNull);
            
        entity.HasOne(e => e.SubKategori)
            .WithMany(sc => sc.MasterIklans)
            .HasForeignKey(e => e.SubKategoriId)
            .OnDelete(DeleteBehavior.SetNull);
            
        entity.HasMany(e => e.Files)
            .WithOne(f => f.Iklan)
            .HasForeignKey(f => f.IklanId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigureUploaderFile(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<UploaderFile>();
        
        // Configure primary key
        entity.HasKey(e => e.FileUploadId);
        
        // Configure table
        entity.ToTable("RealEstateUploaderFiles", "dbo");
        
        // Configure properties
        entity.Property(e => e.FileUploadId)
            .HasColumnName("FileUploadId")
            .ValueGeneratedOnAdd();
            
        entity.Property(e => e.FileName)
            .IsRequired()
            .HasMaxLength(255);
            
        entity.Property(e => e.FilePath)
            .IsRequired()
            .HasMaxLength(500);
            
        entity.Property(e => e.FileType)
            .HasMaxLength(100);
            
        entity.Property(e => e.FileExtension)
            .HasMaxLength(50);
            
        entity.Property(e => e.OriginalFileName)
            .HasMaxLength(255);
            
        entity.Property(e => e.ContentType)
            .HasMaxLength(100);
            
        entity.Property(e => e.UploadStatus)
            .HasMaxLength(20);
            
        entity.Property(e => e.EntityType)
            .HasMaxLength(50);
            
        entity.Property(e => e.UploadDate)
            .HasColumnType("datetime2");

        // Configure relationships
        entity.HasOne(e => e.Iklan)
            .WithMany(i => i.Files)
            .HasForeignKey(e => e.IklanId)
            .OnDelete(DeleteBehavior.SetNull);
    }

    private void ConfigureRealEstateEntities(ModelBuilder modelBuilder)
    {
        // Configure RealEstateCategory
        var categoryEntity = modelBuilder.Entity<RealEstateCategory>();
        categoryEntity.ToTable("RealEstateCategory", "dbo");
        categoryEntity.HasKey(e => e.Id);
        
        categoryEntity.Property(e => e.CategoryName)
            .IsRequired()
            .HasMaxLength(100);
            
        categoryEntity.Property(e => e.CategoryDescription)
            .HasMaxLength(255);
            
        categoryEntity.Property(e => e.CategoryCode)
            .HasMaxLength(50);
            
        categoryEntity.Property(e => e.CategoryIcon)
            .HasMaxLength(255);
            
        categoryEntity.Property(e => e.CategoryColor)
            .HasMaxLength(7);

        categoryEntity.HasOne(e => e.ParentCategory)
            .WithMany(e => e.SubCategories)
            .HasForeignKey(e => e.ParentCategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        // Configure RealEstateSubCategory
        var subCategoryEntity = modelBuilder.Entity<RealEstateSubCategory>();
        subCategoryEntity.ToTable("RealEstateSubCategory", "dbo");
        subCategoryEntity.HasKey(e => e.Id);
        
        subCategoryEntity.Property(e => e.SubCategoryName)
            .IsRequired()
            .HasMaxLength(100);
            
        subCategoryEntity.Property(e => e.SubCategoryDescription)
            .HasMaxLength(255);
            
        subCategoryEntity.Property(e => e.SubCategoryCode)
            .HasMaxLength(50);
            
        subCategoryEntity.Property(e => e.SubCategoryIcon)
            .HasMaxLength(255);
            
        subCategoryEntity.Property(e => e.SubCategoryColor)
            .HasMaxLength(7);

        subCategoryEntity.HasOne(e => e.Category)
            .WithMany(c => c.RealEstateSubCategories)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure RealEstateOwner
        var ownerEntity = modelBuilder.Entity<RealEstateOwner>();
        ownerEntity.ToTable("RealEstateOwner", "dbo");
        ownerEntity.HasKey(e => e.Id);
        
        ownerEntity.Property(e => e.OwnerName)
            .IsRequired()
            .HasMaxLength(100);
            
        ownerEntity.Property(e => e.OwnerEmail)
            .HasMaxLength(100);
            
        ownerEntity.Property(e => e.OwnerPhone)
            .HasMaxLength(20);
            
        ownerEntity.Property(e => e.OwnerAddress)
            .HasMaxLength(255);
            
        ownerEntity.Property(e => e.OwnerIdNumber)
            .HasMaxLength(50);
            
        ownerEntity.Property(e => e.OwnerIdType)
            .HasMaxLength(20);
            
        ownerEntity.Property(e => e.OwnerBirthDate)
            .HasColumnType("datetime2");
            
        ownerEntity.Property(e => e.OwnerGender)
            .HasMaxLength(10);
            
        ownerEntity.Property(e => e.OwnerOccupation)
            .HasMaxLength(50);
            
        ownerEntity.Property(e => e.OwnerCompany)
            .HasMaxLength(100);
            
        ownerEntity.Property(e => e.Notes)
            .HasMaxLength(255);

        // Configure RealEstatePriceRange
        var priceRangeEntity = modelBuilder.Entity<RealEstatePriceRange>();
        priceRangeEntity.ToTable("RealEstatePriceRange", "dbo");
        priceRangeEntity.HasKey(e => e.Id);
        
        priceRangeEntity.Property(e => e.PriceRangeName)
            .IsRequired()
            .HasMaxLength(100);
            
        priceRangeEntity.Property(e => e.MinPrice)
            .HasColumnType("decimal(18,2)");
            
        priceRangeEntity.Property(e => e.MaxPrice)
            .HasColumnType("decimal(18,2)");
            
        priceRangeEntity.Property(e => e.Description)
            .HasMaxLength(255);
            
        priceRangeEntity.Property(e => e.PriceRangeCode)
            .HasMaxLength(50);
            
        priceRangeEntity.Property(e => e.Currency)
            .HasMaxLength(10);

        // Configure RealEstateLocation
        var locationEntity = modelBuilder.Entity<RealEstateLocation>();
        locationEntity.ToTable("RealEstateLocation", "dbo");
        locationEntity.HasKey(e => e.Id);
        
        locationEntity.Property(e => e.LocationName)
            .IsRequired()
            .HasMaxLength(100);
            
        locationEntity.Property(e => e.LocationCode)
            .HasMaxLength(50);
            
        locationEntity.Property(e => e.LocationType)
            .HasMaxLength(50);
            
        locationEntity.Property(e => e.FullAddress)
            .HasMaxLength(255);
            
        locationEntity.Property(e => e.PostalCode)
            .HasMaxLength(10);
            
        locationEntity.Property(e => e.Latitude)
            .HasColumnType("decimal(10,6)");
            
        locationEntity.Property(e => e.Longitude)
            .HasColumnType("decimal(10,6)");
            
        locationEntity.Property(e => e.Province)
            .HasMaxLength(50);
            
        locationEntity.Property(e => e.City)
            .HasMaxLength(50);
            
        locationEntity.Property(e => e.District)
            .HasMaxLength(50);
            
        locationEntity.Property(e => e.Village)
            .HasMaxLength(50);

        locationEntity.HasOne(e => e.ParentLocation)
            .WithMany(e => e.SubLocations)
            .HasForeignKey(e => e.ParentLocationId)
            .OnDelete(DeleteBehavior.SetNull);

        // Configure RealEstateAmenity
        var amenityEntity = modelBuilder.Entity<RealEstateAmenity>();
        amenityEntity.ToTable("RealEstateAmenity", "dbo");
        amenityEntity.HasKey(e => e.Id);
        
        amenityEntity.Property(e => e.AmenityName)
            .IsRequired()
            .HasMaxLength(100);
            
        amenityEntity.Property(e => e.AmenityDescription)
            .HasMaxLength(255);
            
        amenityEntity.Property(e => e.AmenityCode)
            .HasMaxLength(50);
            
        amenityEntity.Property(e => e.AmenityCategory)
            .HasMaxLength(50);
            
        amenityEntity.Property(e => e.AmenityIcon)
            .HasMaxLength(255);
            
        amenityEntity.Property(e => e.Notes)
            .HasMaxLength(255);

        // Configure RealEstate
        var realEstateEntity = modelBuilder.Entity<RealEstate>();
        realEstateEntity.ToTable("RealEstate", "dbo");
        realEstateEntity.HasKey(e => e.Id);
        
        realEstateEntity.Property(e => e.PropertyTitle)
            .IsRequired()
            .HasMaxLength(100);
            
        realEstateEntity.Property(e => e.PropertyPrice)
            .HasColumnType("decimal(18,2)");
            
        realEstateEntity.Property(e => e.PropertyStatus)
            .HasMaxLength(50);
            
        realEstateEntity.Property(e => e.PropertyType)
            .HasMaxLength(20);
            
        realEstateEntity.Property(e => e.LandSize)
            .HasColumnType("decimal(10,2)");
            
        realEstateEntity.Property(e => e.BuildingSize)
            .HasColumnType("decimal(10,2)");
            
        realEstateEntity.Property(e => e.CertificateType)
            .HasMaxLength(50);
            
        realEstateEntity.Property(e => e.PropertyCondition)
            .HasMaxLength(20);
            
        realEstateEntity.Property(e => e.PropertyOrientation)
            .HasMaxLength(50);
            
        realEstateEntity.Property(e => e.RoadAccess)
            .HasMaxLength(100);
            
        realEstateEntity.Property(e => e.WaterSource)
            .HasMaxLength(50);
            
        realEstateEntity.Property(e => e.Latitude)
            .HasColumnType("decimal(10,6)");
            
        realEstateEntity.Property(e => e.Longitude)
            .HasColumnType("decimal(10,6)");
            
        realEstateEntity.Property(e => e.MainImage)
            .HasMaxLength(255);
            
        realEstateEntity.Property(e => e.ListingDate)
            .HasColumnType("datetime2");
            
        realEstateEntity.Property(e => e.ExpiryDate)
            .HasColumnType("datetime2");
            
        realEstateEntity.Property(e => e.AdditionalNotes)
            .HasMaxLength(255);

        // Configure relationships for RealEstate
        realEstateEntity.HasOne(e => e.Category)
            .WithMany()
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
            
        realEstateEntity.HasOne(e => e.SubCategory)
            .WithMany()
            .HasForeignKey(e => e.SubCategoryId)
            .OnDelete(DeleteBehavior.SetNull);
            
        realEstateEntity.HasOne(e => e.Owner)
            .WithMany(o => o.RealEstates)
            .HasForeignKey(e => e.OwnerId)
            .OnDelete(DeleteBehavior.SetNull);
            
        realEstateEntity.HasOne(e => e.Location)
            .WithMany(l => l.RealEstates)
            .HasForeignKey(e => e.LocationId)
            .OnDelete(DeleteBehavior.SetNull);
            
        realEstateEntity.HasOne(e => e.PriceRange)
            .WithMany(pr => pr.RealEstates)
            .HasForeignKey(e => e.PriceRangeId)
            .OnDelete(DeleteBehavior.SetNull);

        // Configure RealEstateAmenityMapping
        var mappingEntity = modelBuilder.Entity<RealEstateAmenityMapping>();
        mappingEntity.ToTable("RealEstateAmenityMapping", "dbo");
        mappingEntity.HasKey(e => e.Id);
        
        mappingEntity.Property(e => e.Notes)
            .HasMaxLength(255);
            
        mappingEntity.Property(e => e.AmenityValue)
            .HasMaxLength(50);

        mappingEntity.HasOne(e => e.RealEstate)
            .WithMany(re => re.RealEstateAmenityMappings)
            .HasForeignKey(e => e.RealEstateId)
            .OnDelete(DeleteBehavior.Cascade);
            
        mappingEntity.HasOne(e => e.Amenity)
            .WithMany(a => a.RealEstateAmenityMappings)
            .HasForeignKey(e => e.AmenityId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigureGlobalQueryFilters(ModelBuilder modelBuilder)
    {
        // Apply query filters for multi-tenancy to all TenantBaseEntity derived types
        // This ensures that all queries are automatically filtered by TenantId
        
        // Note: In a real application, you would get the current tenant from a service
        // For now, this is a placeholder showing how to structure the query filters
        
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(TenantBaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                // This is a placeholder - in a real implementation you would use a tenant service
                // modelBuilder.Entity(entityType.ClrType).HasQueryFilter(...);
            }
        }
    }

    private void ConfigureIndexes(ModelBuilder modelBuilder)
    {
        // MasterIklan indexes
        modelBuilder.Entity<MasterIklan>()
            .HasIndex(e => e.TenantId)
            .HasDatabaseName("IX_MasterIklan_TenantId");
            
        modelBuilder.Entity<MasterIklan>()
            .HasIndex(e => e.StatusIklan)
            .HasDatabaseName("IX_MasterIklan_StatusIklan");
            
        modelBuilder.Entity<MasterIklan>()
            .HasIndex(e => e.IsActive)
            .HasDatabaseName("IX_MasterIklan_IsActive");
            
        modelBuilder.Entity<MasterIklan>()
            .HasIndex(e => e.KategoriId)
            .HasDatabaseName("IX_MasterIklan_KategoriId");

        // UploaderFile indexes
        modelBuilder.Entity<UploaderFile>()
            .HasIndex(e => e.TenantId)
            .HasDatabaseName("IX_UploaderFile_TenantId");
            
        modelBuilder.Entity<UploaderFile>()
            .HasIndex(e => e.IklanId)
            .HasDatabaseName("IX_UploaderFile_IklanId");
            
        modelBuilder.Entity<UploaderFile>()
            .HasIndex(e => e.EntityType)
            .HasDatabaseName("IX_UploaderFile_EntityType");

        // RealEstate indexes
        modelBuilder.Entity<RealEstate>()
            .HasIndex(e => e.TenantId)
            .HasDatabaseName("IX_RealEstate_TenantId");
            
        modelBuilder.Entity<RealEstate>()
            .HasIndex(e => e.PropertyStatus)
            .HasDatabaseName("IX_RealEstate_PropertyStatus");
            
        modelBuilder.Entity<RealEstate>()
            .HasIndex(e => e.IsActive)
            .HasDatabaseName("IX_RealEstate_IsActive");
            
        modelBuilder.Entity<RealEstate>()
            .HasIndex(e => e.CategoryId)
            .HasDatabaseName("IX_RealEstate_CategoryId");

        // Additional performance indexes
        modelBuilder.Entity<RealEstateCategory>()
            .HasIndex(e => e.TenantId)
            .HasDatabaseName("IX_RealEstateCategory_TenantId");
            
        modelBuilder.Entity<RealEstateSubCategory>()
            .HasIndex(e => e.TenantId)
            .HasDatabaseName("IX_RealEstateSubCategory_TenantId");
            
        modelBuilder.Entity<RealEstateOwner>()
            .HasIndex(e => e.TenantId)
            .HasDatabaseName("IX_RealEstateOwner_TenantId");
            
        modelBuilder.Entity<RealEstatePriceRange>()
            .HasIndex(e => e.TenantId)
            .HasDatabaseName("IX_RealEstatePriceRange_TenantId");
            
        modelBuilder.Entity<RealEstateLocation>()
            .HasIndex(e => e.TenantId)
            .HasDatabaseName("IX_RealEstateLocation_TenantId");
            
        modelBuilder.Entity<RealEstateAmenity>()
            .HasIndex(e => e.TenantId)
            .HasDatabaseName("IX_RealEstateAmenity_TenantId");
            
        modelBuilder.Entity<RealEstateAmenityMapping>()
            .HasIndex(e => e.TenantId)
            .HasDatabaseName("IX_RealEstateAmenityMapping_TenantId");
    }
}