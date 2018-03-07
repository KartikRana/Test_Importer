using Dapper;
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
    class Import_Hierarchy_View : WorkSheetImporterAbstractClass
    {
        List<string> validColumnNames = new List<string>();

        public override void Dispose()
        {
            base.Dispose();
            validColumnNames = null;
        }


        public override string GetClientOid()
        {
            //Client: APV Client One
            return "337E56C5-E61A-4390-86B1-27129C82D0D1";
        }


        public override string GetWorksheetName()
        {
            return "Hierarchy View";
        }
        

        public override string GetTvpParameter()
        {
            return "dbo.AssetHierarchyTVP";
        }


        public override string GetProcName()
        {
            return "dbo.Import_AssetHierarchy";
        }


        public override List<string> GetValidColumnNames()
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
        

    }
}
