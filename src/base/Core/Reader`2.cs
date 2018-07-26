namespace Masha.Foundation
{
    using System;

    public class Reader<R, T> 
    {
        public Func<R, T> Run;

        public Reader(Func<R, T> run)
        {
            this.Run = run;
        }
    }
}
