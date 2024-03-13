using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specification
{
    public interface ISpecification<T> where T : BaseEntities
    {
        public Expression<Func<T, bool>> Criteria { get; set; } // Where 
        public List<Expression<Func<T, object>>> IncludeS { get; set; } //include 

        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDescending { get; set; }

        public int  Skip { get; set; }
        public int  take { get; set; }
        public bool IsPagenationEnabled { get; set; }

    }
}
