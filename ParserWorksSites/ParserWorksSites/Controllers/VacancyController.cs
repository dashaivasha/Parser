using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ParserWorksSites.Data.DbContexts;
using ParserWorksSites.Data.Models;
using ParserWorksSites.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParserWorksSites.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class VacancyController : ControllerBase
    {
        private readonly IVacancyService _vacancyService;

        public VacancyController(IVacancyService vacancyService)
        {
            _vacancyService = vacancyService;
        }

        [HttpGet("AllVacancies")]
        public async Task<IEnumerable<Vacancy>> GetAllVacancies()
        {
            return await _vacancyService.GetAllVacancies();
        }

        [HttpGet("Parser")]
        public async Task StartParser()
        {
            await _vacancyService.StartParsing();
        }

        [HttpGet("{id}")]
        public async Task<Vacancy> GetVacancyById(int id)
        {
            return await _vacancyService.GetVacancyById(id);
        }

        [HttpGet("byType")]
        public async Task<IEnumerable<Vacancy>> GetVacanciesByTypeAsync(string type)
        {
            return await _vacancyService.GetSortedVacanciesByTypeAsync(type);
        }
    }
}
