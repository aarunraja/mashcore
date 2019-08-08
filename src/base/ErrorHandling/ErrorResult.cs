using System;
using System.Collections.Generic;
using System.Text;

namespace Masha.Foundation
{
    public class ErrorResult
    {
        public ErrorResult(int code, string message)
        {
            Code = code;
            Message = message;
        }
        public int Code { get; set; }
        public String Message { get; set; }

    }
}
