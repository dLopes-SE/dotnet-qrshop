﻿using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Models.Identity;

namespace dotnet_qrshop.Features.Identity.Login;

public sealed record LoginUserCommand(AuthRequest Request) : ICommand<AuthResponse>;