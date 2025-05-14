using dotnet_qrshop.Common.Results;

namespace dotnet_qrshop.Common.Messaging;

public interface IQueryHandler<in TQuery, TResponse>
  where TQuery : IQuery<TResponse>
{
  Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
}
