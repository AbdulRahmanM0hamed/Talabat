using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specification;

namespace Talabat.Controllers
{

    public class EmployeeController : ApiBaseController
    {
        private readonly IGenaricRepositort<Employee> emprepo;

        public EmployeeController(IGenaricRepositort<Employee> _emprepo)
        {
            emprepo = _emprepo;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmplyees()
        {
            var spec = new EmployeeSpecification();
            var Employees = emprepo.GetAllWithSpecAsync(spec);
            return Ok(Employees);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmplyees(int id)
        {
            var spec = new EmployeeSpecification(id);
            var Employee = emprepo.GetAllWithSpecAsync(spec);
            return Ok(Employee);

        }
    }
}

