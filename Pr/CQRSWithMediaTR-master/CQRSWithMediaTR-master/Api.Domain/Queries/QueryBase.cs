using MediatR;

namespace Customer.Domain.Queries
{
    public abstract class QueryBase<TResult> : IRequest<TResult> 
    {
        
    }
}