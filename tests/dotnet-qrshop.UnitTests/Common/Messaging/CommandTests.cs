using dotnet_qrshop.Common.Messaging;
using dotnet_qrshop.Common.Results;
using Microsoft.Extensions.DependencyInjection;

#region Sample Commands and Handlers

public class SampleCommand : ICommand<string>
{
  public string Value { get; set; }
}

public class SampleCommandHandler : ICommandHandler<SampleCommand, string>
{
  public Task<Result<string>> Handle(SampleCommand command, CancellationToken cancellationToken)
  {
    if (command is null)
      return Task.FromResult(Result.Failure<string>(Error.NullValue));

    return Task.FromResult(Result.Success($"Handled: {command.Value}"));
  }
}

public class CancellableCommand : ICommand<string> { }

public class CancellableCommandHandler : ICommandHandler<CancellableCommand, string>
{
  public static CancellationToken CapturedToken;

  public Task<Result<string>> Handle(CancellableCommand command, CancellationToken cancellationToken)
  {
    CapturedToken = cancellationToken;
    return Task.FromResult(Result.Success("Cancelled OK"));
  }
}

public class FailingCommand : ICommand<string> { }

public class FailingCommandHandler : ICommandHandler<FailingCommand, string>
{
  public Task<Result<string>> Handle(FailingCommand command, CancellationToken cancellationToken)
      => Task.FromResult(Result.Failure<string>(Error.Failure("Invalid Operation", "Something went wrong")));
}

#endregion

public class CommandTests
{
  [Fact, Trait("Common", "Messaging")]
  public async Task SampleCommandHandler_Should_Handle_Command_Correctly()
  {
    var services = new ServiceCollection();
    services.AddScoped<ICommandHandler<SampleCommand, string>, SampleCommandHandler>();
    var provider = services.BuildServiceProvider();

    var handler = provider.GetRequiredService<ICommandHandler<SampleCommand, string>>();
    var result = await handler.Handle(new SampleCommand { Value = "Test" }, CancellationToken.None);

    Assert.True(result.IsSuccess);
    Assert.Equal("Handled: Test", result.Value);
  }

  [Fact, Trait("Common", "Messaging")]
  public async Task CancellableCommandHandler_Should_Receive_CancellationToken()
  {
    var services = new ServiceCollection();
    services.AddScoped<ICommandHandler<CancellableCommand, string>, CancellableCommandHandler>();
    var provider = services.BuildServiceProvider();

    var handler = provider.GetRequiredService<ICommandHandler<CancellableCommand, string>>();

    var cts = new CancellationTokenSource();
    var token = cts.Token;

    var result = await handler.Handle(new CancellableCommand(), token);

    Assert.True(result.IsSuccess);
    Assert.Equal(token, CancellableCommandHandler.CapturedToken);
  }

  [Fact, Trait("Common", "Messaging")]
  public async Task FailingCommandHandler_Should_Return_Failure_Result()
  {
    var services = new ServiceCollection();
    services.AddScoped<ICommandHandler<FailingCommand, string>, FailingCommandHandler>();
    var provider = services.BuildServiceProvider();

    var handler = provider.GetRequiredService<ICommandHandler<FailingCommand, string>>();
    var result = await handler.Handle(new FailingCommand(), CancellationToken.None);

    Assert.False(result.IsSuccess);
    Assert.Equal("Something went wrong", result.Error?.Description);
    Assert.Equal("Invalid Operation", result.Error?.Code);
  }

  [Fact, Trait("Common", "Messaging")]
  public async Task Null_Command_Should_Return_NullValue_Error()
  {
    var services = new ServiceCollection();
    services.AddScoped<ICommandHandler<SampleCommand, string>, SampleCommandHandler>();
    var provider = services.BuildServiceProvider();

    var handler = provider.GetRequiredService<ICommandHandler<SampleCommand, string>>();

    var result = await handler.Handle(null, CancellationToken.None);

    Assert.False(result.IsSuccess);
    Assert.Equal(Error.NullValue.Code, result.Error?.Code);
    Assert.Equal(Error.NullValue.Description, result.Error?.Description);
  }
}
