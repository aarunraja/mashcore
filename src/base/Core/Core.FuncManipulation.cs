namespace Masha.Foundation.Base.Core
{
    using System;

    public static partial class Core
    {
        public static R Pipe<T, R>(this T t, Func<T, R> f) => f(t);
    }
}
