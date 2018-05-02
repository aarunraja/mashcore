namespace Masha.Foundation
{
    using System;
    using System.Threading.Tasks;

    public static partial class Core
    {
        public static Task<T> Async<T>(T value) => Task.FromResult<T>(value);
        public static Task<T> Async<T>(Exception exception) => Task.FromException<T>(exception);
        public static Task<T> Async<T>(Error exception) => Async<T>(exception);

        public static R Pipe<T, R>(this T t, Func<T, R> f) => f(t);


        public async static Task<Result<R>> Pipe<T, R>(this T t, Func<T, Task<R>> f)
        {
            R result = default(R);
            try
            {
                result = await f(t);
            }
            catch (Exception ex)
            {
                return Result<R>(Error.Of(ex));
            }
            return result;
        }

        public async static Task<Result<R>> Pipe<T, R>(this T t, Func<T, Task<Result<R>>> f)
        {
            Result<R> result = default(R);
            try
            {
                result = await f(t);
            }
            catch (Exception ex)
            {
                return Result<R>(Error.Of(ex));
            }
            return result;
        }

        // TRt -> TRr
        public async static Task<Result<R>> Map<T, R>(this Task<Result<T>> task, Func<T, Task<Result<R>>> f)
        {
            Result<T> inwardResult = null;
            Result<R> outwardResult = null;
            try
            {
                inwardResult = await task.ConfigureAwait(continueOnCapturedContext: false);
                if(inwardResult.HasValue)
                {
                    outwardResult = await f(inwardResult.value);
                }else
                {
                    outwardResult = inwardResult.error;
                }
            }
            catch (Exception ex)
            {
                outwardResult = Error.Of(ex);
            }
            return outwardResult;
        }

        // Tt -> TRr
        public async static Task<Result<R>> Map<T, R>(this Task<T> task, Func<T, Task<Result<R>>> f)
        {
            T inwardResult = default(T);
            Result<R> outwardResult = null;
            try
            {
                inwardResult = await task.ConfigureAwait(continueOnCapturedContext: false);
                outwardResult = await f(inwardResult);
            }
            catch (Exception ex)
            {
                outwardResult = Error.Of(ex);
            }
            return outwardResult;
        }

        // Tt -> Tr -> TRr
        public async static Task<Result<R>> Map<T, R>(this Task<T> task, Func<T, Task<R>> f)
        {
            T inwardResult = default(T);
            R outwardResult = default(R);
            try
            {
                inwardResult = await task.ConfigureAwait(continueOnCapturedContext: false);
                outwardResult = await f(inwardResult);
            }
            catch (Exception ex)
            {
                return Result<R>(Error.Of(ex));
            }
            return outwardResult;
        }

        // TRt -> Tt -> TRr
        public async static Task<Result<R>> Map<T, R>(this Task<Result<T>> task, Func<T, R> f)
        {
            Result<T> inwardResult = null;
            R outwardResult = default(R);
            try
            {
                inwardResult = await task.ConfigureAwait(continueOnCapturedContext: false);
                if (inwardResult.HasValue)
                {
                    outwardResult = f(inwardResult.value);
                }
                else
                {
                    return inwardResult.error;
                }
            }
            catch (Exception ex)
            {
                return Result<R>(Error.Of(ex));
            }
            return outwardResult;
        }

        // Rt -> TRr
        public async static Task<Result<R>> Map<T, R>(this Result<T> result, Func<T, Task<Result<R>>> f)
        {
            Result<R> outwardResult = null;
            try
            {                
                if (result.HasValue)
                {
                    outwardResult = await f(result.value);
                }
                else
                {
                    return result.error;
                }
            }
            catch (Exception ex)
            {
                return Result<R>(Error.Of(ex));
            }
            return outwardResult;
        }
    }
}
