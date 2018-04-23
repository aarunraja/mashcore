namespace Masha.Foundation
{
    using System;
    using System.Threading.Tasks;

    public static partial class Core
    {
        public static Task<T> Async<T>(T value) => Task.FromResult<T>(value);
        public static Task<T> Async<T>(Exception exception) => Task.FromException<T>(exception);
        //TODO
        //public static Task<T> Async<T>(Error exception) => Task.FromException<T>(exception);

        public async static Task<Result<R>> Map<T, R>(this Task<Result<T>> task, Func<T, Result<R>> f)
        {
            Result<T> inwardResult = null;
            Result<R> outwardResult = null;
            try
            {
                inwardResult = await task.ConfigureAwait(continueOnCapturedContext: false);
                if(inwardResult.HasValue)
                {
                    outwardResult = f(inwardResult.value);
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
    }
}
