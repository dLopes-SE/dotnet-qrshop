using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_qrshop.Entities;

public abstract class BaseEntity
{
  public int Id { get; set; }
  [Column("created_at")]
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  [Column("updated_at")]
  public DateTime? UpdatedAt { get; set; }
}
