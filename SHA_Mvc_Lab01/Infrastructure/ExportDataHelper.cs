using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
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

        public static DataTable GetExportDataTable<T>(IEnumerable<T> source,
                                                      string selectedColumns) where T : class
        {
            var exportSource = GetExportDataFromSource(source, selectedColumns);

            var exportData = JsonConvert.DeserializeObject<DataTable>(exportSource.ToString());

            return exportData;
        }

        private static JArray GetExportDataFromSource<T>(IEnumerable<T> source,
                                                string selectedColumns) where T : class
        {
            var jobjects = new JArray();

            var columns = selectedColumns.Split(',');

            var exportColumns = ExportColumnAttributeHelper<T>
                .GetExportColumns()
                .Where(x => columns.Contains(x.ColumnName))
                .ToList();

            foreach (var item in source)
            {
                Type type = typeof(T);
                var pinfos = type.GetProperties();

                var rowDatas = new List<Tuple<int, string, object>>();

                foreach (var pinfo in pinfos)
                {
                    var cinfo = exportColumns.FirstOrDefault(x => x.ColumnName == pinfo.Name);
                    if (cinfo == null)
                    { 
                        continue;
                    }

                    rowDatas.Add(new Tuple<int, string, object>(
                        item1: cinfo.Order,
                        item2: cinfo.Name,
                        item3: pinfo.GetValue(item) ?? string.Empty));
                }

                var jo = new JObject();
                foreach (var row in rowDatas.OrderBy(x => x.Item1))
                {
                    jo.Add(row.Item2, JToken.FromObject(row.Item3));
                }
                jobjects.Add(jo);
            }

            return jobjects;
        }
    }
}