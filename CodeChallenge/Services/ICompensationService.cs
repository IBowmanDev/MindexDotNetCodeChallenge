using CodeChallenge.DTOs;
using CodeChallenge.Models;

namespace CodeChallenge.Services
{
    public interface ICompensationService
    {
        Compensation Create(Compensation compensation);
        Compensation GetByEmployeeId(string employeeId);
    }
}