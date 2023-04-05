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

        [HttpGet]
        public async Task<IEnumerable<Vacancy>> GetAllVacancies(string site, string type = null, int? page = null, int? pageSize = null)
        {
            return await _vacancyService.GetAllVacancies(site, type, page, pageSize);
        }

        [HttpGet("{id}")]
        public async Task<Vacancy> GetVacancyById(int id)
        {
            return await _vacancyService.GetVacancyById(id);
        }

        [HttpGet("type/{type}")]
        public async Task<IEnumerable<Vacancy>> GetVacanciesByTypeAsync(string type)
        {
            return await _vacancyService.GetVacanciesByTypeAsync(type);
        }

    }
}
