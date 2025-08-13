using System.ComponentModel.DataAnnotations;

namespace HapusIsiDatabase.Models;

/// <summary>
/// Base entity class for multi-tenant entities
/// </summary>
public abstract class TenantBaseEntity : BaseEntity
{
    [Required]
    public int TenantId { get; set; }
}