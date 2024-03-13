using Talabat.Core.Entities;
using Talabat.Core.Specification;

namespace Talabat.Core.Specification
{
    public class EmployeeSpecification : BaseSpecification<Employee>
    {
        public EmployeeSpecification()
        {
            IncludeS.Add(P => P.Department);

        }

        public EmployeeSpecification(int id) : base(E => E.Id == id)
        {
            IncludeS.Add(P => P.Department);

        }
    }
}
