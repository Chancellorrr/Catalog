using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.IO;
using Newtonsoft.Json;

namespace Catalog
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var program = new CatalogProgram();
            program.Start();
        }

        
    }
}
