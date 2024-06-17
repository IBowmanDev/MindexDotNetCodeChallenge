using CodeChallenge.Models;
using CodeChallenge.Repositories;
using System.Threading.Tasks;

namespace CodeChallenge.Helpers.Mappers
{
    public static class ReportingStructureMapper
    {
        public static ReportingStructureDto MapToDto(ReportingStructure reportingStructure)
        {
            return new ReportingStructureDto
            {
                EmployeeId = reportingStructure.Employee.EmployeeId,
                NumberOfReports = reportingStructure.NumberOfReports
            };
        }
    }
}