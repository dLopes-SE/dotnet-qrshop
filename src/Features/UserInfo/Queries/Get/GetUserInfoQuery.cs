using dotnet_qrshop.Abstractions.Messaging;

namespace dotnet_qrshop.Features.UserInfo.Queries.Get;

public sealed record GetUserInfoQuery : IQuery<UserInfoDto>;
