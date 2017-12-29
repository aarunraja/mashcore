namespace Masha.Foundation.Tests
{
    using System;  
    using Masha.Foundation;
    using static Masha.Foundation.Core;

    public static class General 
    {
        public static Option<int> ToInt(string value)
        {
            int result;
            if (int.TryParse(value, out result))
            {
                return result;
            }
            else
            {
                return None;
            }
        }

        public static Option<double> ToDouble(int value) => (double)value;
    }
}