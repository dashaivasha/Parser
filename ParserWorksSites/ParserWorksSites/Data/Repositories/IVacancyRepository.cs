using ParserWorksSites.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParserWorksSites.Data.Repositories
{
    public interface IVacancyRepository
    {
        Task<Vacancy> GetVacancyById(int id);
        Task<IEnumerable<Vacancy>> GetAllVacancies();
        Task<IEnumerable<Vacancy>> GetVacanciesByTypeAsync(string type);
        Task CreateVacancyAsync(Vacancy vacancy);

    }
}
