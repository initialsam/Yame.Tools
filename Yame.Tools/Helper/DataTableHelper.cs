using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using Yame.Tools.Extensions;

namespace Yame.Tools.Helper
{
    public static class DataTableHelper
    {
        public static List<string> GetColumnNameList(DataTable dataTable)
        {
            List<string> columnNameList = new List<string>();
            foreach (DataColumn column in dataTable.Columns)
            {
                columnNameList.Add(column.ColumnName);
            }
            return columnNameList;
        }

        public static string ConvertDataTableToHTML(DataTable dataTable)
        {
            string html = "<table class='table table-striped'>";
            //add header row
            html += "<tr>";
            for (int i = 0; i < dataTable.Columns.Count; i++)
                html += "<th>" + dataTable.Columns[i].ColumnName + "</th>";
            html += "</tr>";
            //add rows
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dataTable.Columns.Count; j++)
                    html += "<td>" + dataTable.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }

        public static DataTable ConvertTo<T>(Dictionary<string, T> list, string dataTableName)
        {
            DataTable table = CreateTable1<T>(dataTableName);
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (KeyValuePair<string, T> item in list)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    if (prop.PropertyType.Name != "Dictionary`2")
                    {
                        if (prop.PropertyType.FullName == "System.String")
                            if (prop.GetValue(item.Value) == null)
                                row[prop.Name] = prop.GetValue(item.Value);
                            else
                                row[prop.Name] = prop.GetValue(item.Value).ToString().Replace("'", "''");
                        else
                            row[prop.Name] = prop.GetValue(item.Value);
                    }
                table.Rows.Add(row);
            }
            return table;
        }

        public static DataTable CreateTable1<T>(string dataTableName)
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(dataTableName);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            return table;
        }
        public static string GetFirstValidRawData(DataTable dataTable, string mappingColumnName, ThirdPartyIdentityType identityType)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (DataColumn column in dataTable.Columns)
                {
                    var rowData = row[column].ToString();
                    if (string.IsNullOrEmpty(rowData) ||
                        (
                            identityType == ThirdPartyIdentityType.MAC &&
                            rowData.HasEnglish() == false
                        ))
                        continue;
                    //儲存一份原始格式的資料 EX 18-DB-F2-5E-45-F8
                    if (column.ColumnName == mappingColumnName)
                    {
                        return rowData;
                    }
                }
            }
            return string.Empty;
        }
    }
    /// <summary>
    /// 指出第三方軟體資訊應 mapping 至 Pixis 主機資訊的 Key
    /// </summary>
    public enum ThirdPartyIdentityType
    {
        IP,
        MAC,
    }
}
