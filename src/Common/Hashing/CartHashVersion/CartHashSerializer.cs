﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace dotnet_qrshop.Common.Hashing.CartHashVersion;

public static class CartHashSerializer
{
  public static string SerializeDeterministically(CartVersionPayload payload)
  {
    var orderedPayload = new CartVersionPayload(
      payload.CartId,
      [.. payload.Items
          .OrderBy(i => i.Id)
          .ThenBy(i => i.ItemId)
          .ThenBy(i => i.Quantity)]
    );

    return JsonSerializer.Serialize(orderedPayload, _options);
  }

  private static readonly JsonSerializerOptions _options = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = false,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
  };
}
