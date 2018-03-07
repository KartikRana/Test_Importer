using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Importer.ImporterFolder
{
    public class ErrorRow
    {
        public int RowNumber { get; set; }
        public string ErrorMessage { get; set; }
        public string CreatedOn { get; set; }
        public string Level { get; set; }
        public string GCRecord { get; set; }
    }
}
