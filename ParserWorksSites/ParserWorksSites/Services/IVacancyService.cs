using ParserWorksSites.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParserWorksSites.Services
{
    public interface IVacancyService
    {
        Task<IEnumerable<Vacancy>> GetAllVacancies();
        Task<IEnumerable<Vacancy>> GetVacanciesByTypeAsync(string type);

        Task<Vacancy> GetVacancyById(int id);
        Task StartParsing();
        Task<IEnumerable<Vacancy>> GetSortedVacanciesByTypeAsync(string site, string sortType, string sortOrder, int? page = null, int? pageSize = null);
    }
}
