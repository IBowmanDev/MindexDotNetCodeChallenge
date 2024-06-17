using CodeChallenge.DTOs;
using CodeChallenge.Helpers.Mappers;
using CodeChallenge.Models;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;

        public CompensationService(ICompensationRepository compensationRepository) 
        {
            _compensationRepository = compensationRepository;
        }

        public Compensation GetByEmployeeId(string employeeId)
        {
            if(employeeId != null)
            {
                return _compensationRepository.GetById(employeeId);
            }

            return null;
        }

        public Compensation Create(Compensation compensation)
        {
            if (compensation != null)
            {
                _compensationRepository.Add(compensation);
                _compensationRepository.SaveAsync().Wait();

                return compensation;
            }

            return null;
        }

    }
}
