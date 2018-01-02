namespace Masha.Foundation
{
    using System;

    public static partial class Core
    {
        public static Option<T> Some<T>(T value) => new Option<T>(value);
        public static readonly NoneObject None = NoneObject._;

        public static R Match<T, R>(this Option<T> option,
            Func<T,R> Some, Func<R> None) =>
                option.HasSome ? Some(option.Value) : None();

        public static Option<R> EMap<T, R>(this Option<T> option,
            Func<T, R> f) =>
                option.Match((t) => Some(f(t)), () => None);
    }
}
