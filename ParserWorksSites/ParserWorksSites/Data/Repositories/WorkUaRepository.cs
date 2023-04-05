using Microsoft.EntityFrameworkCore;
using ParserWorksSites.Data.DbContexts;
using ParserWorksSites.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParserWorksSites.Data.Repositories
{
    public class WorkUaRepository : IVacancyRepository
    {
        private readonly MyDbContext _dbContext;

        public WorkUaRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Vacancy>> GetAllVacancies()
        {
            return await _dbContext.Vacancy.ToListAsync();
        }

        public async Task<Vacancy> GetVacancyById(int id)
        {
            return _dbContext.Vacancy.FirstOrDefault(v => v.Id == id);
        }

        public async Task<IEnumerable<Vacancy>> GetVacanciesByTypeAsync(string type)
        {
            return await _dbContext.Vacancy
                .Where(v => v.Type == type)
                .ToListAsync();
        }

        public async Task CreateVacancyAsync(Vacancy vacancy)
        {
            _dbContext.Vacancy.Add(vacancy);
            await _dbContext.SaveChangesAsync();
        }
    }
}

