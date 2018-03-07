﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Test_Importer.ImporterFolder
{
    class Import_Hierarchy_View : IWorksheetImport
    {
        List<string> validColumnNames = new List<string>();
        DataTable tableToInsert = new DataTable();
        List<string> ValidColumnNamesCompare = new List<string>();
        List<string> columnDifferences = new List<string>();

        public void Dispose()
        {
            validColumnNames = null;
            ValidColumnNamesCompare = null;
            columnDifferences = null;
            tableToInsert.Dispose();
        }


        public string GetClientOid()
        {
            //Client: APV Client One
            return "337E56C5-E61A-4390-86B1-27129C82D0D1";
        }


        public string GetWorksheetName()
        {
            return "Hierarchy View";
        }
        

        public string GetTvpParameter()
        {
            return "dbo.AssetHierarchyTVP";
        }


        public string GetProcName()
        {
            return "dbo.Import_AssetHierarchy";
        }


        public List<string> GetValidColumnNames()
        {
            validColumnNames = new List<string>();
            validColumnNames.Add("AssetClass");
            validColumnNames.Add("AssetType");
            validColumnNames.Add("AssetSubType");
            validColumnNames.Add("Component");
            validColumnNames.Add("ComponentType");
            validColumnNames.Add("ComponentSubType");
            validColumnNames.Add("Code");
            return validColumnNames;
        }


        public bool ValidateWorksheetName(string name)
        {
            return name == GetWorksheetName();
        }


        public bool VlidateWorksheetColumns(List<string> inputColumnNames)
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


        public DataTable PrepareTableToInsert(DataTable tableFromSheet)
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


    }
}
