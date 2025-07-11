﻿using dotnet_qrshop.Abstractions.Messaging;
using dotnet_qrshop.Common.Models.Identity;

namespace dotnet_qrshop.Features.Identity.Register;

public sealed record RegisterUserCommand(RegistrationRequest Request)
  : ICommand<RegistrationResponse>;