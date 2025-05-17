using dotnet_qrshop.Common.Messaging;
using dotnet_qrshop.Common.Results;
using Microsoft.Extensions.DependencyInjection;

#region Sample Queries and Handlers

public class SampleQuery : IQuery<string>
{
  public string Value { get; set; }
}

public class SampleQueryHandler : IQueryHandler<SampleQuery, string>
{
  public Task<Result<string>> Handle(SampleQuery query, CancellationToken cancellationToken)
  {
    if (query is null)
      return Task.FromResult(Result.Failure<string>(Error.NullValue));

    return Task.FromResult(Result.Success($"Handled: {query.Value}"));
  }
}

public class CancellableQuery : IQuery<string> { }

public class CancellableQueryHandler : IQueryHandler<CancellableQuery, string>
{
  public static CancellationToken CapturedToken;

  public Task<Result<string>> Handle(CancellableQuery query, CancellationToken cancellationToken)
  {
    CapturedToken = cancellationToken;
    return Task.FromResult(Result.Success("Cancelled OK"));
  }
}

public class FailingQuery : IQuery<string> { }

public class FailingQueryHandler : IQueryHandler<FailingQuery, string>
{
  public Task<Result<string>> Handle(FailingQuery query, CancellationToken cancellationToken)
      => Task.FromResult(Result.Failure<string>(Error.Failure("Invalid Operation", "Something went wrong")));
}

#endregion

public class QueryTests
{
  [Fact, Trait("Common", "Messaging")]
  public async Task SampleQueryHandler_Should_Handle_Query_Correctly()
  {
    var services = new ServiceCollection();
    services.AddScoped<IQueryHandler<SampleQuery, string>, SampleQueryHandler>();
    var provider = services.BuildServiceProvider();

    var handler = provider.GetRequiredService<IQueryHandler<SampleQuery, string>>();
    var result = await handler.Handle(new SampleQuery { Value = "Test" }, CancellationToken.None);

    Assert.True(result.IsSuccess);
    Assert.Equal("Handled: Test", result.Value);
  }

  [Fact, Trait("Common", "Messaging")]
  public async Task CancellableQueryHandler_Should_Receive_CancellationToken()
  {
    var services = new ServiceCollection();
    services.AddScoped<IQueryHandler<CancellableQuery, string>, CancellableQueryHandler>();
    var provider = services.BuildServiceProvider();

    var handler = provider.GetRequiredService<IQueryHandler<CancellableQuery, string>>();

    var cts = new CancellationTokenSource();
    var token = cts.Token;

    var result = await handler.Handle(new CancellableQuery(), token);

    Assert.True(result.IsSuccess);
    Assert.Equal(token, CancellableQueryHandler.CapturedToken);
  }

  [Fact, Trait("Common", "Messaging")]
  public async Task FailingQueryHandler_Should_Return_Failure_Result()
  {
    var services = new ServiceCollection();
    services.AddScoped<IQueryHandler<FailingQuery, string>, FailingQueryHandler>();
    var provider = services.BuildServiceProvider();

    var handler = provider.GetRequiredService<IQueryHandler<FailingQuery, string>>();
    var result = await handler.Handle(new FailingQuery(), CancellationToken.None);

    Assert.False(result.IsSuccess);
    Assert.Equal("Something went wrong", result.Error?.Description);
    Assert.Equal("Invalid Operation", result.Error?.Code);
  }

  [Fact, Trait("Common", "Messaging")]
  public async Task Null_Query_Should_Return_NullValue_Error()
  {
    var services = new ServiceCollection();
    services.AddScoped<IQueryHandler<SampleQuery, string>, SampleQueryHandler>();
    var provider = services.BuildServiceProvider();

    var handler = provider.GetRequiredService<IQueryHandler<SampleQuery, string>>();

    var result = await handler.Handle(null, CancellationToken.None);

    Assert.False(result.IsSuccess);
    Assert.Equal(Error.NullValue.Code, result.Error?.Code);
    Assert.Equal(Error.NullValue.Description, result.Error?.Description);
  }
}
