using Microsoft.EntityFrameworkCore;
using HapusIsiDatabase.Data;
using HapusIsiDatabase.Models;
using Xunit;

namespace HapusIsiDatabase.Tests;

/// <summary>
/// Unit tests to validate Entity Framework models and configurations
/// </summary>
public class ModelValidationTests
{
    /// <summary>
    /// Create an in-memory database context for testing
    /// </summary>
    private ApplicationDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    /// <summary>
    /// Test that MasterIklan model can be created with custom primary key
    /// </summary>
    [Fact]
    public void MasterIklan_CanBeCreated_WithCustomPrimaryKey()
    {
        // Arrange & Act
        var iklan = new MasterIklan
        {
            TenantId = 1,
            JudulIklan = "Test Iklan",
            DeskripsiIklan = "Test Description",
            HargaIklan = 1000000m,
            StatusIklan = "Active",
            TipeIklan = "Jual",
            IsActive = true
        };

        // Assert
        Assert.NotNull(iklan);
        Assert.Equal("Test Iklan", iklan.JudulIklan);
        Assert.Equal(1000000m, iklan.HargaIklan);
        Assert.True(iklan.IsActive);
        Assert.Equal(1, iklan.TenantId);
    }

    /// <summary>
    /// Test that UploaderFile model can be created with custom primary key
    /// </summary>
    [Fact]
    public void UploaderFile_CanBeCreated_WithCustomPrimaryKey()
    {
        // Arrange & Act
        var file = new UploaderFile
        {
            TenantId = 1,
            FileName = "test.jpg",
            FilePath = "/uploads/test.jpg",
            FileSize = 1024,
            FileType = "image",
            FileExtension = ".jpg",
            IsMainImage = true
        };

        // Assert
        Assert.NotNull(file);
        Assert.Equal("test.jpg", file.FileName);
        Assert.Equal("/uploads/test.jpg", file.FilePath);
        Assert.Equal(1024, file.FileSize);
        Assert.True(file.IsMainImage);
        Assert.Equal(1, file.TenantId);
    }

    /// <summary>
    /// Test that RealEstateCategory can be created properly
    /// </summary>
    [Fact]
    public void RealEstateCategory_CanBeCreated()
    {
        // Arrange & Act
        var category = new RealEstateCategory
        {
            TenantId = 1,
            CategoryName = "Rumah",
            CategoryDescription = "Kategori untuk rumah",
            CategoryCode = "HSE",
            IsActive = true,
            SortOrder = 1
        };

        // Assert
        Assert.NotNull(category);
        Assert.Equal("Rumah", category.CategoryName);
        Assert.Equal("HSE", category.CategoryCode);
        Assert.True(category.IsActive);
        Assert.Equal(1, category.TenantId);
    }

    /// <summary>
    /// Test that the ApplicationDbContext can be created and models are properly configured
    /// </summary>
    [Fact]
    public void ApplicationDbContext_CanBeCreated_WithInMemoryDatabase()
    {
        // Arrange & Act
        using var context = CreateInMemoryContext();

        // Assert
        Assert.NotNull(context);
        Assert.NotNull(context.MasterIklans);
        Assert.NotNull(context.UploaderFiles);
        Assert.NotNull(context.RealEstateCategories);
        Assert.NotNull(context.RealEstateSubCategories);
        Assert.NotNull(context.RealEstateOwners);
        Assert.NotNull(context.RealEstatePriceRanges);
        Assert.NotNull(context.RealEstateLocations);
        Assert.NotNull(context.RealEstateAmenities);
        Assert.NotNull(context.RealEstates);
        Assert.NotNull(context.RealEstateAmenityMappings);
    }

    /// <summary>
    /// Test that models can be saved and retrieved from database
    /// </summary>
    [Fact]
    public void Models_CanBeSavedAndRetrieved_FromDatabase()
    {
        // Arrange
        using var context = CreateInMemoryContext();

        var category = new RealEstateCategory
        {
            TenantId = 1,
            CategoryName = "Rumah",
            CategoryCode = "HSE",
            IsActive = true,
            SortOrder = 1
        };

        var iklan = new MasterIklan
        {
            TenantId = 1,
            JudulIklan = "Rumah Dijual",
            DeskripsiIklan = "Rumah bagus di Jakarta",
            HargaIklan = 2000000000m,
            StatusIklan = "Available",
            TipeIklan = "Jual",
            IsActive = true
        };

        // Act
        context.RealEstateCategories.Add(category);
        context.MasterIklans.Add(iklan);
        context.SaveChanges();

        // Assert
        var savedCategory = context.RealEstateCategories.First(c => c.CategoryName == "Rumah");
        var savedIklan = context.MasterIklans.First(i => i.JudulIklan == "Rumah Dijual");

        Assert.NotNull(savedCategory);
        Assert.NotNull(savedIklan);
        Assert.Equal("Rumah", savedCategory.CategoryName);
        Assert.Equal("Rumah Dijual", savedIklan.JudulIklan);
        Assert.Equal(2000000000m, savedIklan.HargaIklan);
    }

    /// <summary>
    /// Test that primary key conflicts are resolved properly
    /// </summary>
    [Fact]
    public void MasterIklan_UsesIdIklanAsPrimaryKey()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var entityType = context.Model.FindEntityType(typeof(MasterIklan));

        // Act
        var primaryKey = entityType?.FindPrimaryKey();

        // Assert
        Assert.NotNull(primaryKey);
        Assert.Single(primaryKey.Properties);
        Assert.Equal("IdIklan", primaryKey.Properties.First().Name);
    }

    /// <summary>
    /// Test that UploaderFile uses FileUploadId as primary key
    /// </summary>
    [Fact]
    public void UploaderFile_UsesFileUploadIdAsPrimaryKey()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var entityType = context.Model.FindEntityType(typeof(UploaderFile));

        // Act
        var primaryKey = entityType?.FindPrimaryKey();

        // Assert
        Assert.NotNull(primaryKey);
        Assert.Single(primaryKey.Properties);
        Assert.Equal("FileUploadId", primaryKey.Properties.First().Name);
    }
}