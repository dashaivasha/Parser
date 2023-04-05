using AngleSharp;
using ParserWorksSites.Data.DbContexts;
using ParserWorksSites.Data.Models;
using ParserWorksSites.Data.Repositories;
using ParserWorksSites.Parsers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParserWorksSites.Services
{
    public class VacancyService : IVacancyService
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly MyDbContext _dbContext;

        public VacancyService(IVacancyRepository vacancyRepository, MyDbContext dbContext)
        {
            _vacancyRepository = vacancyRepository;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Vacancy>> GetAllVacancies()
        {
            return await _vacancyRepository.GetAllVacancies();
        }

        public async Task<Vacancy> GetVacancyById(int id)
        {
            return await _vacancyRepository.GetVacancyByIdAsync(id);
        }

        public async Task<IEnumerable<Vacancy>> GetSortedVacanciesByTypeAsync(string type)
        {
            var allVacancies = await _vacancyRepository.GetAllVacancies();
            var sortedVacancies = allVacancies
                .OrderBy(v => v.Type)
                .ToList();
            return sortedVacancies;
        }

        public async Task StartParsing()
        {
            var site = "https://www.work.ua/jobs/?ss=";
            var parentLink = "https://www.work.ua";

            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);

            var totalPages = await WorkUaParser.GetTotalPages(site + "1");

            for (int i = 1; i <= totalPages; i++)
            {
                var url = site + i.ToString();
                var document = await context.OpenAsync(url);

                var vacanciesDivs = document.QuerySelectorAll("div.card-hover");

                var vacancies = await WorkUaParser.GetObjectsFromDivs(vacanciesDivs, parentLink);
                await _vacancyRepository.AddVacanciesAsync(Task.FromResult(vacancies));


            }
        }

    }

}

