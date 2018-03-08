using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Test_Importer.ImporterFolder
{
    public static class ExcelPackageClass
    {   
        //Create a DataTable from excel sheet
        public static DataTable GetDataTableFromSheet(ExcelWorksheet sheet)
        {
            var tableFromSheet = new DataTable();
            int totalColumns = sheet.Dimension.Columns;
            int totalRows = sheet.Dimension.Rows;

            for (int row = 1; row <= totalRows; row++)
            {
                string[] currentRowData = new string[totalColumns];
                DataRow dr = tableFromSheet.NewRow();
                int arrayPosition = 0;
                for (int col = 1; col <= totalColumns; col++)
                {
                    if (row == 1)
                    {
                        if (sheet.Cells[row, col].Value == null)
                        {
                            tableFromSheet.Columns.Add(string.Empty, typeof(string));
                        }
                        else
                        {
                            string cellToAdd = sheet.Cells[row, col].Value.ToString().Replace("%","Percentage");
                            cellToAdd = Regex.Replace(cellToAdd, "[^\\w\\._]", "");
                            tableFromSheet.Columns.Add(cellToAdd.Trim().ToLower().Replace(" ", ""), typeof(string));
                        }
                    }
                    else
                    {
                        if (sheet.Cells[row, col].Value == null)
                        {
                            currentRowData[arrayPosition] = string.Empty;
                        }
                        else
                        {
                            currentRowData[arrayPosition] = sheet.Cells[row, col].Value.ToString();
                        }
                        arrayPosition++;
                    }
                }
                if (row != 1)
                {
                    tableFromSheet.Rows.Add(currentRowData);
                }
            }
            return tableFromSheet;
        }
    }
}
