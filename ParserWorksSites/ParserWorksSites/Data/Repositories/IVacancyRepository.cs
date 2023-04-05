﻿using ParserWorksSites.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParserWorksSites.Data.Repositories
{
    public interface IVacancyRepository
    {
        Task<Vacancy?> GetVacancyByIdAsync(int id);
        Task<IEnumerable<Vacancy>> GetAllVacancies();
        Task AddVacanciesAsync(Task<IEnumerable<Vacancy>> vacancies);
    }
}
