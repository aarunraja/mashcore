using System;
using System.Collections.Generic;
using System.Text;

namespace Masha.Foundation.Contract
{
    public class PagedResults<T>
    {
        public byte[] PageStatus { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalNumberOfPages { get; set; }
        public int TotalNumberOfRecords { get; set; }
        public string NextPageUrl { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}
