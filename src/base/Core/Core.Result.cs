namespace Masha.Foundation
{
    using System;

    public static partial class Core
    {
        public static Result<T> Result<T>(T value) => new Result<T>(value);
        public static Result<T> Result<T>(Error error) => new Result<T>(error);

        public static S Match<T, S>(this Result<T> result,
            Func<T, S> pass, Func<Error, S> fail) =>
            result.HasValue ? pass(result.value) : fail(result.error);

        public static S Match<S>(this Result result,
            Func<S> pass, Func<Error, S> fail) =>
            result.HasValue ? pass() : fail(result.error);

        public static Result<S> Map<T, S>(this Result<T> result,
            Func<T, S> f) => result.HasValue ? Result(f(result.value)) : result.error;
        
        public static Result<S> Map<S>(this Result result,
            Func<S> f) => result.HasValue ? Result(f()) : result.error;
    }
}
