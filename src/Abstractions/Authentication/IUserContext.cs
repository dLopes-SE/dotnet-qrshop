﻿namespace dotnet_qrshop.Abstractions.Authentication;

public interface IUserContext
{
  Guid UserId { get; }
}
