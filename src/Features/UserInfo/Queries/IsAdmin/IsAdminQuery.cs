using dotnet_qrshop.Abstractions.Messaging;

namespace dotnet_qrshop.Features.UserInfo.Queries.IsAdmin;

public sealed record IsAdminQuery : IQuery<bool>;
