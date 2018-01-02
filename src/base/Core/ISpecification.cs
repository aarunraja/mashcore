namespace Masha.Foundation
{
    using System;
    using System.Linq.Expressions;

    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Predicate { get; }
    }
}
