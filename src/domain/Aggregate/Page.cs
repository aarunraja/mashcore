
namespace Masha.Foundation.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Page<T>  where T : DomainEntity
    {
        public byte[] PageStatus { get; set; }
        public int Size { get; set; }
        public int Number { get; set; }
        public ulong TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> Records { get; set; }
    }
}
