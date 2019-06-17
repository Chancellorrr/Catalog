using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Catalog
{
    class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public long Size { get; set; }
        public string Location { get; set; }

    }
}
