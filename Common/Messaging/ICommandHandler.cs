using dotnet_qrshop.Common.Results;

namespace dotnet_qrshop.Common.Messaging;

public interface ICommandHandler<in TCommand>
  where TCommand : ICommand
{
  Task<Result> Handle(TCommand command, CancellationToken cancellationToken);
}

public interface ICommandHandler<in TCommand, TResponse>
  where TCommand : ICommand<TResponse>
{
  Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken);
}