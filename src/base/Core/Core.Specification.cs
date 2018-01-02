namespace Masha.Foundation
{
    using System;
    using System.Linq.Expressions;

    public static partial class Core
    {
        public static Specification<T> Spec<T>(Expression<Func<T, bool>> predicate) => new Specification<T>(predicate);
        public static S Match<T, S>(this Specification<T> spec,
            T value, Func<T, S> pass, Func<S> fail)
        {
            var predicateCompiled = spec.predicate.Compile();
            return predicateCompiled.Invoke(value) ? pass(value) : fail();
        }
    }
}
