using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Test_Importer.ImporterFolder
{
    public class Import_FairValue_Measurement_Class : WorkSheetImporterAbstractClass
    {
        List<string> validColumnNames = new List<string>();

        public override void Dispose()
        {
            validColumnNames = null;
            base.Dispose();
        }

        public override string GetClientOid()
        {
            //Client: APV Client One
            return "337E56C5-E61A-4390-86B1-27129C82D0D1";
        }

        public override string GetProcName()
        {
            return "dbo.Import_FairValueMeasurementHierarchy";
        }

        public override string GetTvpParameter()
        {
            return "dbo.FairValueMeasurementHierarchyTVP";
        }

        public override List<string> GetValidColumnNames()
        {
            return validColumnNames = new List<string>()
            {
                "FinancialAssetClass",
                "FairValueMeasurementClass",
                "FairValueMeasurementApproach",
                "FairValueMeasurementHierarchy"
            };
        }

        public override string GetWorksheetName()
        {
            return "Fair Value Measurement Class";
        }
    }
}
