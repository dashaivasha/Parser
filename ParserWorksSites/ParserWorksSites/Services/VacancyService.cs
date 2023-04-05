using ParserWorksSites.Data.Models;
using ParserWorksSites.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using ParserWorksSites.Services;
using System.Linq;
using System;
using AngleSharp;
using ParserWorksSites.Parsers;
using ParserWorksSites.Data.DbContexts;

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

        public async Task<IEnumerable<Vacancy>> GetAllVacancies(string site, string type = null, int? page = null, int? pageSize = null)
        {
            return await _vacancyRepository.GetAllVacancies();
        }

        public async Task<IEnumerable<Vacancy>> GetVacanciesByTypeAsync(string type) 
        {
            return await _vacancyRepository.GetVacanciesByTypeAsync(type);
        }

        public async Task<Vacancy> GetVacancyById(int id)
        {
            return await _vacancyRepository.GetVacancyById(id);
        }

        public async Task<IEnumerable<Vacancy>> GetSortedVacanciesByTypeAsync(string site, string sortType, string sortOrder, int? page = null, int? pageSize = null)
        {
            var allVacancies = await _vacancyRepository.GetAllVacancies();
            var sortedVacancies = allVacancies
                .OrderBy(v => v.Title)
                .ToList();
            return sortedVacancies;
        }

        public async Task StartParsing()
        {
            var site = "https://www.work.ua/jobs-kyiv-it/?page=";
            var type = "IT";
            var parentLink = "https://www.work.ua";

            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);

            var totalPages = await WorkUaParser.GetTotalPages(site + "1");

            for (int i = 1; i <= totalPages; i++)
            {
                var url = site + i.ToString();
                var document = await context.OpenAsync(url);

                var vacanciesDivs = document.QuerySelectorAll("div.card-hover");

                var vacancies = WorkUaParser.GetObjectsFromDivs(vacanciesDivs, type, parentLink).ToList();

                foreach (var vacancy in vacancies)
                {
                    var existingVacancy = await _vacancyRepository.GetVacanciesByTypeAsync(vacancy.Type);

                    if (existingVacancy == null)
                    {
                        await _vacancyRepository.CreateVacancyAsync(vacancy);
                    }
                }

                await _dbContext.SaveChangesAsync();
            }
        }


    }

}

