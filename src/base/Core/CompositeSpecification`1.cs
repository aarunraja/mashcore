namespace Masha.Foundation
{
    using System;

    public class CompositeSpecification<T> : Specification<T>
    {
        public Specification<T> LeftSpec { get; }
        public Specification<T> RightSpec { get; }

        public CompositeSpecification(Specification<T> leftSpec, Specification<T> rightSpec)
        {
            this.LeftSpec = leftSpec;
            this.RightSpec = rightSpec;
        }
    }
}
