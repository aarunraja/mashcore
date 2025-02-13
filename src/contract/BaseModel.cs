﻿using System;

namespace Masha.Foundation.Contract
{
    public class BaseModel
    {
        public string Id { get; set; }
        public virtual DateTimeOffset CreatedOn { get; set; }
        public virtual String CreatedBy { get; set; }
        public virtual DateTimeOffset ModifiedOn { get; set; }
        public virtual String ModifiedBy { get; set; }
        public bool IsDelete { get; set; }
    }
}