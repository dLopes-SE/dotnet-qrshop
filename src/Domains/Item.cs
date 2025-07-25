﻿using dotnet_qrshop.Common.Enums;
using dotnet_qrshop.Features.Items;

namespace dotnet_qrshop.Domains;

public class Item : BaseEntity
{
  public string Name { get; set; }
  public string Slogan { get; set; }
  public string Description { get; set; }
  public string Image { get; set; }
  public ShopItemCategoryEnum Category { get; set; }
  public bool IsFeaturedItem { get; set; }
  /// <summary>
  /// Dollar
  /// </summary>
  public double Price { get; set; }

  public ICollection<CartItem> CartItems { get; set; } = [];

  public static explicit operator Item(ItemRequest request) =>
    new()
    {
      Name = request.Name,
      Slogan = request.Slogan,
      Description = request.Description,
      Image = request.Image,
      IsFeaturedItem = request.IsFeaturedItem,
      Price = request.Price,
    };
}
