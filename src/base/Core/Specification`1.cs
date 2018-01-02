namespace Masha.Foundation
{
    using System;
    using System.Linq.Expressions;

    public class Specification<T>
    {
        internal Expression<Func<T, bool>> predicate;

        public Specification(Expression<Func<T, bool>> predicate)
        {
            this.predicate = predicate;
        }
    }
}
