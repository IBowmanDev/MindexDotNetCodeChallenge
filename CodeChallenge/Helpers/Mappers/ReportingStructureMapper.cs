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
                Employee = EmployeeMapper.MapToDto(reportingStructure.Employee),
                NumberOfReports = reportingStructure.NumberOfReports
            };
        }
    }
}