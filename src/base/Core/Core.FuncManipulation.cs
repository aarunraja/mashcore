namespace Masha.Foundation.Base.Core
{
    using System;
    using System.Threading.Tasks;

    public static partial class Core
    {
        public static R Pipe<T, R>(this T t, Func<T, R> f) => f(t);
        public static Task<Result<R>> Pipe<T, R>(this T t, Func<T, Task<Result<R>>> f) => f(t);
    }
}
