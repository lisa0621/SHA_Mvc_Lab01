using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHA_Mvc_Lab01.Infrastructure
{
    public class ExportColumnAttribute : Attribute
    {
        /// <summary>
        /// 欄位顯示名稱.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// 欄位顯示順序.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public int Order { get; set; }

        public ExportColumnAttribute()
        {
        }
    }
}