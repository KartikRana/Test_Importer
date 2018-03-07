using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Importer.ImporterFolder
{
    public static class ConstantsClass
    {
        public static string ConnectionString
        {
            get
            {
                string conn = @"Integrated Security=SSPI;Pooling=false;Data Source=(localdb)\mssqllocaldb;Initial Catalog=fvp-database_Copy";
                return conn;
            }
        }
    }
}
