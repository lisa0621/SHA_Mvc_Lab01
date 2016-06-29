using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHA_Mvc_Lab01.Infrastructure
{
    public class CheckResult
    {
        public Guid ID { get; set; }

        public bool Success { get; set; }

        public int RowCount { get; set; }

        public int ErrorCount { get; set; }

        public string ErrorMessage { get; set; }
    }
}