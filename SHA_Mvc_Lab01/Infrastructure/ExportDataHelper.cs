using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHA_Mvc_Lab01.Infrastructure
{
    public class ExportDataHelper
    {
        /// <summary>
        /// Team_Rider instance.
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> Team_RiderExportColumns()
        {
            var result = new Dictionary<string, string>();

            result.Add("Year", "Year");
            result.Add("Class", "Class");
            result.Add("TeamName", "Team Name");
            result.Add("Company", "Company");
            result.Add("RiderName", "Rider Name");
            result.Add("Country", "Country");
            result.Add("Age", "Age");

            return result;
        }

        public static List<string> GetRemoveColumnNames(Dictionary<string, string> allColumns, string selectedColumns)
        {
            var allColumnValues = allColumns.Select(x => x.Key).ToList();
            var columns = selectedColumns.Split(',').ToList();

            //移除未被選取的欄位
            var removeColumns = allColumnValues.Except(columns).ToList();

            return removeColumns;
        }
    }
}