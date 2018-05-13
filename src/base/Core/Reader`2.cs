namespace Masha.Foundation
{
    using System;

    public class Reader<R, T> 
    {
        internal Func<R, T> run;

        internal Reader(Func<R, T> run)
        {
            this.run = run;
        }
    }
}
