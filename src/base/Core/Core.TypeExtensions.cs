namespace Masha.Foundation
{
    using System;
    using System.Collections.Generic;

    public static partial class Core
    {
        #region Dict
        public static Option<V> Get<K, V>(this IDictionary<K, V> dict, K key)
        {
            V value;
            return dict.TryGetValue(key, out value) ? Some(value) : None;
        }
        #endregion
    }
}
