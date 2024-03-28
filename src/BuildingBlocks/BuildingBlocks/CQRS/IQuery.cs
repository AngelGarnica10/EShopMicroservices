using MediatR;

namespace BuildingBlocks.CQRS
{
    //internal interface IQuery<out TResponse> : IRequest<TResponse>
    //{
    //}

    public interface IQuery<out TResponse> : IRequest<TResponse>
        where TResponse : notnull
    {
    }
}
