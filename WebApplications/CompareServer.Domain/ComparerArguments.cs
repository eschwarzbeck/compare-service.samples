using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompareServer.Domain.ComparerProxy;

namespace CompareServer.Domain
{
    public class ComparerArguments
    {
        #region data members
        public ServerFile OriginalDoc { get; set; }
        public List<ServerFile> ModifiedDoc { get; set; }

        public string RenderingSet { get; set; }
        public ResponseOptions OutputFormat { get; set; }

       // public string RootPath { get; set; }
        #endregion
    }
}
