namespace Masha.Foundation
{
    using System;

    public static partial class Core
    {
        public static Result<T> Result<T>(T value) => new Result<T>(value);
        public static Result<T> Result<T>(Error error) => new Result<T>(error);
        public static Result<T> AsResult<T>(this T t) => Result(t);

        public static S Match<T, S>(this Result<T> result,
            Func<T, S> pass, Func<Error, S> fail) =>
            result.HasValue ? pass(result.value) : fail(result.error);

        public static S Match<S>(this Result result,
            Func<S> pass, Func<Error, S> fail) =>
            result.HasValue ? pass() : fail(result.error);

        public static T GetOrElse<T>(this Result<T> result, T alt) => result.HasValue ? result.value : alt;

        #region Result<T> Map
        public static Result<T> Map<T>(this Result<T> result,
            Specification<T> spec, Func<Error> fail)
        {
            if (result.HasValue)
            {
                var predicateCompiled = spec.predicate.Compile();
                return predicateCompiled.Invoke(result.value) ? result : fail();
            }
            return result.error;
        }

        public static Result<S> Map<T, S>(this Result<T> result,
            Func<T, Result<S>> f)
        {
            if (result.HasValue)
            {
                return f(result.value);
            }
            else
            {
                return result.error;
            }
        }


        #endregion

        #region Monadic Stack
        //public static Result<T> Map<T>(this Result<T> result,
        //    Func<T, Result> f)
        //{
        //    if (result.HasValue)
        //    {
        //        var result2 = f(result.value);
        //        if (result2.HasValue) return result;
        //        else return result2.error;
        //    }
        //    else
        //    {
        //        return result.error;
        //    }
        //}
        public static Result<R> Map<T, R>(this Result<T> result,
            Func<T, Option<R>> f)
        {
            if (result.HasValue)
            {
                return f(result.value);
            }
            else
            {
                return Error.SomeError;
            }
        }
        #endregion

        #region Result Map
        //public static Result<S> Map<S>(this Result result,
        //    Func<S> f) => result.HasValue ? Result(f()) : result.error;

        //public static Result Map(this Result result,
        //    Func<Result> f) => result.HasValue ? f() : result.error;

        //public static Result<S> Map<S>(this Result result,
        //    Func<Result<S>> f) => result.HasValue ? f() : result.error;
        #endregion
    }
}
