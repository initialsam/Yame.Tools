using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Yame.Tools.NetCore.FileProcessor
{
    public class ExcelFileUploader
    {
        public DataTable GenerateUploadDataTable(string fileNameFullPath)
        {
            DataTable dt = null;
            try
            {
                dt = new DataTable();
                //載入Excel檔案
                var workbook = new XLWorkbook(fileNameFullPath);
                var ws = workbook.Worksheet(1);
                var range = ws.RangeUsed();
                //// Start Read from (1,A)
                for (int i = 1; i <= range.ColumnCount(); i++)
                {
                    if (ws.Cell(1, i).Value != null)
                    {
                        string cellValue = ws.Cell(1, i).Value.ToString();
                        dt.Columns.Add(cellValue);
                    }
                }
                for (int i = 2; i < range.RowCount() + 1; i++)
                {
                    DataRow dataRow = dt.NewRow();
                    for (int j = 1; j < range.ColumnCount() + 1; j++)
                    {
                        if (ws.Cell(i, j).Value != null)
                        {
                            string cellValue = ws.Cell(i, j).Value.ToString();
                            dataRow[j - 1] = cellValue;
                        }
                    }
                    dt.Rows.Add(dataRow);
                }
                workbook.Dispose();
                return dt;
               
            }
            catch (DuplicateNameException ex)
            {
                //_log.Warn("ExcelFileUploader.GenerateUploadDataTable Error : " + ex);
                throw;
            }
            catch (Exception ex)
            {
                //_log.WarnFormat("ExcelFileUploader.Excute Error !\r\n{0}", ex.StackTrace);
                return dt;
            }
        }

        public void RemoveAllFiles(string[] picList)
        {
            picList.ToList().ForEach(f =>
            {
                try
                {
                    File.Delete(f);
                }
                catch (Exception e)
                {
                    //_log.WarnFormat("ExcelFileUploader.RemoveAllFiles Error !\r\n{0}", e.StackTrace);
                }
            }
            );
        }
    }
}
