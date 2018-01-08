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

        public static Result<S> Bind<T, S>(this Result<T> result,
            Func<T, S> f) => result.HasValue ? Result(f(result.value)) : result.error;

        public static Result<T> Bind<T>(this Result<T> result,
            Specification<T> spec, Func<Error> fail)
        {
            if (result.HasValue)
            {
                var predicateCompiled = spec.predicate.Compile();
                return predicateCompiled.Invoke(result.value) ? result : fail();
            }
            return result.error;
        }

        public static Result<S> Bind<S>(this Result result,
            Func<S> f) => result.HasValue ? Result(f()) : result.error;

        public static Result<S> Map<T, S>(this Result<T> result,
            Func<T, Result<S>> f) 
        {
            if(result.HasValue)
            {
                var r = f(result.value);
                if(r.GetType() == typeof(Result<Option<S>>))
                {
                    if(r.HasValue)
                    {
                        Option<S> v = r.value;
                        return v.Match(Some: v1 => Result(v1), None: () => Error.SomeError);
                    }else 
                    {
                        return r.error;
                    }
                }else 
                {
                    return r;
                }
            }else 
            { 
                return result.error;
            }
        }

        public static Result<T> Map<T>(this Result<T> result,
            Specification<T> spec, Func<Error> fail)
        {
            if (result.HasValue)
            {
                var predicateCompiled = spec.predicate.Compile();
                return predicateCompiled.Invoke(result.value) ? result :  fail();
            }
            return result.error;
        }

        public static Result<S> Map<S>(this Result result,
            Func<Result<S>> f) => result.HasValue ? f() : result.error;
    }
}
