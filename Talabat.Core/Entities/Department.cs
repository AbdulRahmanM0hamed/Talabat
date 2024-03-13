using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Department:BaseEntities
    {
        public string Name { get; set; }
        public DateOnly CreateDate { get; set; }
    }
}
