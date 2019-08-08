
namespace Masha.Foundation.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Page<T>  where T : DomainEntity
    {
        public int Size { get; }
        public int Number { get; }
        public ulong TotalRecords { get; }
        public int TotalPages { get; }
        public int Count { get; }

        public IEnumerable<T> Records { get; }
        public Page(int number, IEnumerable<T> records, ulong totalRecords, int size = 10)
        {
            Number = number >= 0 ? number : 0;
            Size = size > 0 ? size : 1;

            var count = records.Count();

            if (count > Size)
            {
                Count = Size;
                Records = records.Take(Size);
            }
            else
            {
                Count = count;
                Records = records;
            }

            TotalRecords = totalRecords >= (ulong)count ? totalRecords : (ulong)count;

            TotalPages = (int)Math.Ceiling(TotalRecords / (double)Size);
        }
    }
}
