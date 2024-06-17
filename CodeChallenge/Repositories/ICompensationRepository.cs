using CodeChallenge.Models;
using System;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public interface ICompensationRepository
    {
        Compensation Add(Compensation compensation);
        Compensation GetById(String id);
        Task SaveAsync();
    }
}
