using Dapper;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Test_Importer.ImporterFolder
{
    class ImporterClass : IDisposable
    {
        ExcelPackage package;
        List<IWorksheetImport> worksheetImporterList;
        List<string> inputColumnNames;
        DataTable tableFromSheet;
        List<Tuple<string, string, DataTable>> tupleToImport = new List<Tuple<string, string, DataTable>>();

        public void Dispose()
        {
            package.Dispose();
            worksheetImporterList = null;
            inputColumnNames = null;
            tupleToImport = null;
        }


        public ImporterClass()
        {
            worksheetImporterList = new List<IWorksheetImport>();
            worksheetImporterList.Add(new Import_Hierarchy_View());
            worksheetImporterList.Add(new Import_Financial_Sub_Class());
            worksheetImporterList.Add(new Import_FairValue_Measurement_Class());
        }


        public bool RunValidation(string path)
        {
            try
            {
                bool validationPassed = true;
                if (!string.IsNullOrEmpty(path))
                {
                    package = new ExcelPackage(new FileInfo(path));
                    foreach(IWorksheetImport importer in worksheetImporterList)
                    {
                        using (importer)
                        {
                            foreach (ExcelWorksheet sheet in package.Workbook.Worksheets)
                            {
                                if (importer.ValidateWorksheetName(sheet.Name))
                                {
                                    tableFromSheet = ExcelPackageClass.GetDataTableFromSheet(sheet);
                                    inputColumnNames = new List<string>();
                                    foreach (var columnName in tableFromSheet.Columns)
                                    {
                                        inputColumnNames.Add(columnName.ToString());
                                    }
                                    if (importer.VlidateWorksheetColumns(inputColumnNames))
                                    {
                                        Tuple<string, string, DataTable> tuple = new Tuple<string, string, DataTable>(importer.GetProcName(), importer.GetTvpParameter(), importer.PrepareTableToInsert(tableFromSheet));
                                        tupleToImport.Add(tuple);
                                    }
                                    else
                                    {
                                        validationPassed = false;
                                    }
                                }
                                else
                                {
                                    validationPassed = false;
                                }
                            }
                        }
                    }
                }
                if(tupleToImport.Count > 0)
                {
                    foreach(Tuple<string, string, DataTable> tuple in tupleToImport)
                    {
                        StartImport(tuple.Item1, tuple.Item2, tuple.Item3);
                    }
                }
                return validationPassed;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void StartImport(string ProcName, string TVPName, DataTable tableToInsert)
        {
            using (SqlConnection connection = new SqlConnection(ConstantsClass.ConnectionString))
            {
                connection.Open();
                var errors = connection.Query<ErrorRow>(ProcName, new { Rows = tableToInsert.AsTableValuedParameter(TVPName) },
                commandType: CommandType.StoredProcedure, commandTimeout: 3000);
                foreach (var error in errors.Take(100))
                {
                    OperationLogger.WriteLogToResult(string.Format("At row {0}: {1}", error.RowNumber, error.ErrorMessage, error.CreatedOn, error.Level, error.GCRecord));
                }
            }
        }


    }
}
