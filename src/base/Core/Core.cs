namespace Masha.Foundation
{
    using System;

    public static partial class Core 
    {
        internal static string[] BoxedTypes = new string[] {
            "Masha.Foundation.Option", "Masha.Foundation.Return"
        }; 

        internal static bool IsBoxingType(string typeFullName)
        {
            bool contains = false;
            for(var i = 0; i < BoxedTypes.Length; i++)
            {
                if(typeFullName.Contains(BoxedTypes[i]))
                {
                    contains = true;
                }
            }
            return contains;
        }
    }
}