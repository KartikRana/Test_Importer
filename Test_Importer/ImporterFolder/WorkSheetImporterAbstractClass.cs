using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Test_Importer.ImporterFolder
{
    public abstract class WorkSheetImporterAbstractClass : IWorksheetImport, IDisposable
    {
        public List<string> ValidColumnNamesCompare { get; private set; }
        private List<string> validColumnNames;
        private DataTable tableToInsert;

        public virtual void Dispose()
        {
            validColumnNames = null;
            ValidColumnNamesCompare = null;
            tableToInsert.Dispose();
        }

        public abstract string GetClientOid();
        public abstract string GetProcName();
        public abstract string GetTvpParameter();
        public abstract List<string> GetValidColumnNames();
        public abstract string GetWorksheetName();

        public virtual DataTable PrepareTableToInsert(DataTable tableFromSheet)
        {
            tableToInsert = new DataTable();
            tableToInsert.Columns.Add("RowNumber", typeof(string));
            tableToInsert.Columns.Add("ClientOid", typeof(string));
            foreach (string columnName in validColumnNames)
            {
                tableToInsert.Columns.Add(columnName, typeof(string));
            }

            int rowNo = 1;
            foreach (DataRow row in tableFromSheet.Rows)
            {
                DataRow rowToInsert = tableToInsert.NewRow();
                rowToInsert["RowNumber"] = rowNo.ToString();
                rowToInsert["clientOid"] = GetClientOid();
                foreach (string columnName in ValidColumnNamesCompare)
                {
                    rowToInsert[columnName] = row[columnName];
                }
                tableToInsert.Rows.Add(rowToInsert);
                rowNo++;
            }
            return tableToInsert;
        }

        public virtual bool ValidateWorksheetName(string name)
        {
            return name == GetWorksheetName();
        }

        public virtual bool VlidateWorksheetColumns(List<string> inputColumnNames)
        {
            ValidColumnNamesCompare = new List<string>();
            validColumnNames = GetValidColumnNames();
            foreach (string columnName in validColumnNames)
            {
                string columnNameToAdd = Regex.Replace(columnName, "[^\\w\\._]", "");
                ValidColumnNamesCompare.Add(columnNameToAdd.Trim().ToLower().Replace(" ", ""));
            }

            int countValidColumns = ValidColumnNamesCompare.Count;
            int countVlidColumnPresent = 0;
            foreach (string columnName in ValidColumnNamesCompare)
            {
                if (inputColumnNames.Contains(columnName))
                {
                    countVlidColumnPresent++;
                }
            }

            if (countValidColumns == countVlidColumnPresent) return true;
            return false;
        }
    }
}
