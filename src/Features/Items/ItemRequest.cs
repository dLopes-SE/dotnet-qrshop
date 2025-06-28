namespace dotnet_qrshop.Features.Items;

public record ItemRequest (string Name, string Slogan, string Description, string Image, bool IsFeaturedItem, double Price);