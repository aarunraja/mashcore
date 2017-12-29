namespace Masha.Foundation
{
    using System;

    public class NoneObject
    {
        public static readonly NoneObject _ = new NoneObject();

        private NoneObject()
        { }
    }
}