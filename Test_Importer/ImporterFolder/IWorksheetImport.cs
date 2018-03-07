using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Importer.ImporterFolder
{
    interface IWorksheetImport : IDisposable
    {
        string GetWorksheetName();
        string GetClientOid();
        string GetTvpParameter();
        string GetProcName();
        List<string> GetValidColumnNames();
        bool ValidateWorksheetName(string name);
        bool VlidateWorksheetColumns(List<string> inputColumnNames);
        DataTable PrepareTableToInsert(DataTable tableFromSheet);
    }
}
