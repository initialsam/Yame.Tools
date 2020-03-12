using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Yame.Tools.Helper
{
    public static class ExcelExportHelper
    {
        public static string ExcelContentType
        {
            get
            {
                return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }
        }

        /// <summary>
        /// List轉DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dataTable = new DataTable();
            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }
            object[] values = new object[properties.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = properties[i].GetValue(item);
                }

                dataTable.Rows.Add(values);
            }
            return dataTable;
        }


        /// <summary>
        /// 匯出Excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable">資料集合</param>
        /// <param name="heading">Worksheet</param>
        /// <param name="showSrNo">顯示流水號</param>
        /// <param name="changeBackgroundColorName">相同資料的欄位BackgroundColor顯示一樣的</param>
        /// <param name="columnsToTake">欄位名稱</param>
        /// <returns></returns>
        public static byte[] ExportExcel(DataTable dataTable, string heading = "", bool showSrNo = false, string changeBackgroundColorName = "", params string[] columnsToTake)
        {
            byte[] result = null;
            using (ExcelPackage excel = new ExcelPackage())
            {
                ExcelWorksheet workSheet = excel.Workbook.Worksheets.Add("Data");

                int startRowFrom = string.IsNullOrEmpty(heading) ? 1 : 3;
                //顯示流水號
                if (showSrNo)
                {
                    DataColumn dataColumn = dataTable.Columns.Add("#", typeof(int));
                    dataColumn.SetOrdinal(0);
                    int index = 1;
                    foreach (DataRow item in dataTable.Rows)
                    {
                        item[0] = index;
                        index++;
                    }
                }

                //Add Content Into the Excel File
                workSheet.Cells["A" + startRowFrom].LoadFromDataTable(dataTable, true);
                // autofit width of cells with small content  
                int columnIndex = 1;
                foreach (DataColumn item in dataTable.Columns)
                {
                    ExcelRange columnCells = workSheet.Cells[workSheet.Dimension.Start.Row, columnIndex, workSheet.Dimension.End.Row, columnIndex];
                    int maxLength = columnCells.Max(cell => cell.Value?.ToString().Count() ?? 0);
                    if (maxLength < 150)
                    {
                        workSheet.Column(columnIndex).AutoFit();
                    }
                    columnIndex++;
                }
                if (!String.IsNullOrEmpty(changeBackgroundColorName))
                {
                    columnIndex = 1;
                    foreach (DataColumn item in dataTable.Columns)
                    {
                        ExcelRange columnCells = workSheet.Cells[workSheet.Dimension.Start.Row, columnIndex, workSheet.Dimension.End.Row, columnIndex];
                        if (columnCells.First().Value.ToString() == changeBackgroundColorName)
                        {
                            break;
                        }
                        columnIndex++;
                    }

                    string previousValue = String.Empty;
                    string currentValue = String.Empty;
                    Color backgroundColor = Color.LightGray;
                    for (int row = workSheet.Dimension.Start.Row + 1; row <= workSheet.Dimension.End.Row; row++)
                    {
                        currentValue = workSheet.Cells[row, columnIndex].Value.ToString();
                        ExcelRow rowRange = workSheet.Row(row);
                        if (previousValue != currentValue)
                        {
                            backgroundColor = backgroundColor == Color.LightGray ?
                                               Color.White :
                                               Color.LightGray;
                        }
                        previousValue = currentValue;
                        using (ExcelRange r = workSheet.Cells[row, 1, row, dataTable.Columns.Count])
                        {
                            r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            r.Style.Fill.BackgroundColor.SetColor(backgroundColor);
                        }
                    }
                }

                // format header - bold, yellow on black  
                using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom, dataTable.Columns.Count])
                {
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.Font.Bold = true;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));
                }

                // format cells - add borders  
                using (ExcelRange r = workSheet.Cells[startRowFrom + 1, 1, startRowFrom + dataTable.Rows.Count, dataTable.Columns.Count])
                {
                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                }

                // removed ignored columns  
                for (int i = dataTable.Columns.Count - 1; i >= 0; i--)
                {
                    if (i == 0 && showSrNo)
                    {
                        continue;
                    }
                    if (!columnsToTake.Contains(dataTable.Columns[i].ColumnName))
                    {
                        workSheet.DeleteColumn(i + 1);
                    }
                }

                if (!String.IsNullOrEmpty(heading))
                {
                    workSheet.Cells["A1"].Value = heading;
                    workSheet.Cells["A1"].Style.Font.Size = 20;

                    workSheet.InsertColumn(1, 1);
                    workSheet.InsertRow(1, 1);
                    workSheet.Column(1).Width = 5;
                }

                result = excel.GetAsByteArray();

            }
            return result;
        }

        /// <summary>
        /// 匯出Excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">資料集合</param>
        /// <param name="heading">Worksheet</param>
        /// <param name="isShowSlNo">顯示流水號</param>
        /// <param name="changeBackgroundColorName">相同資料的欄位BackgroundColor顯示一樣的</param>
        /// <param name="ColumnsToTake">欄位名稱</param>
        /// <returns></returns>
        public static byte[] ExportExcel<T>(List<T> data, string heading = "", bool isShowSlNo = false, string changeBackgroundColorName = "", params string[] ColumnsToTake)
        {
            return ExportExcel(ListToDataTable<T>(data), heading, isShowSlNo, changeBackgroundColorName, ColumnsToTake);
        }

    }
}
