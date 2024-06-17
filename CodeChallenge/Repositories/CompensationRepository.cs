using CodeChallenge.Data;
using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace CodeChallenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<ICompensationRepository> _logger;
        public CompensationRepository(EmployeeContext employeeContext, ILogger<ICompensationRepository> logger) 
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Compensation Add(Compensation compensation)
        {
            // generate new guid for compensation id
            compensation.CompensationId = Guid.NewGuid().ToString(); 

            _employeeContext.Compensations.Add(compensation);

            return compensation;
        }

        public Compensation GetById(string id)
        {
            return _employeeContext.Compensations
                .SingleOrDefault(e => e.Employee.EmployeeId == id);
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }
    }
}
