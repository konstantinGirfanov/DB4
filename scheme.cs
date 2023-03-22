using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheme
{
    class Scheme
    {
        public string Name { get; set; }
        public List<SchemeColumn> Columns { get; set; }
    }

    class SchemeColumn
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}