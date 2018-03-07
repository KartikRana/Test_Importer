using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Test_Importer.ImporterFolder
{
    public class Import_Financial_Sub_Class : WorkSheetImporterAbstractClass
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

        public override string GetProcName()
        {
            return "dbo.Import_FinancialSubClass";
        }

        public override string GetTvpParameter()
        {
            return "dbo.FinancialSubClassTVP";
        }

        public override List<string> GetValidColumnNames()
        {
            validColumnNames = new List<string>();
            validColumnNames.Add("FinancialAssetClass");
            validColumnNames.Add("FinancialSubClass");
            return validColumnNames;
        }

        public override string GetWorksheetName()
        {
            return "Financial Sub-Class Report";
        }
        

    }
}
