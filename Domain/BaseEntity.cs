﻿namespace dotnet_qrshop.Domain;

public abstract class BaseEntity
{
  public Guid Id { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; set; }
  public bool IsDeleted { get; set; }

}
