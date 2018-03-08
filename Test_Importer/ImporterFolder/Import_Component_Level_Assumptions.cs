using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Importer.ImporterFolder
{
    class Import_Component_Level_Assumptions : WorkSheetImporterAbstractClass
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
            return "dbo.Import_ComponentLevelAssumptions";
        }

        public override string GetTvpParameter()
        {
            return "dbo.ComponentLevelAssumptionTVP";
        }

        public override List<string> GetValidColumnNames()
        {
            validColumnNames = new List<string>()
            {
                "UnitRate",
                "LocalityFactor",
                "LongLifePerc",
                "UsefulLifeShortMin",
                "UsefulLifeShortMax",
                "RVPercentageShort",
                "UsefulLifeLong",
                "RVPercentageLong",
                "ComponentSubType",
                "ComponentType",
                "Component",
                "Code",
                "AssetSubType",
                "AssetType",
                "AssetClass",
                "DepreciationPolicy"
            };
            return validColumnNames;
        }

        public override string GetWorksheetName()
        {
            return "Component Level Assumptions";
        }
    }
}
