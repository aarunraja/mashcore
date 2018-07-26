namespace Masha.Foundation
{
    using System;

    public static partial class Core
    {
        public static Reader<R, B> Map<R, A, B>(this Reader<R, A> reader, Func<A, B> f)
        {
            return new Reader<R, B>(r => f(reader.Run(r)));
        }

        public static Reader<R, B> FlatMap<R, A, B>(this Reader<R, A> reader, Func<A, Reader<R,B>> f)
        {
            return new Reader<R, B>(r => f(reader.Run(r)).Run(r));
        }
    }
}
