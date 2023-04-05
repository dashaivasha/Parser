using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ParserWorksSites.Data.DbContexts;
using ParserWorksSites.Data.Models;
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
        private readonly MyDbContext _context;

        public VacancyController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Vacancy> Get()
        {
            return _context.Vacancy.ToList();
        }

        [HttpGet("{id}")]
        public Vacancy Get(int id)
        {
            return _context.Vacancy.Find(id);
        }

        [HttpPost]
        public void Post(Vacancy vacancy)
        {
            _context.Vacancy.Add(vacancy);
            _context.SaveChanges();
        }

        [HttpPut("{id}")]
        public void Put(int id, Vacancy vacancy)
        {
            if (id != vacancy.Id)
            {
                return BadRequest();
            }

            _context.Entry(vacancy).State = EntityState.Modified;
            _context.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var vacancy = _context.Vacancy.Find(id);
            if (vacancy == null)
            {
                return NotFound();
            }

            _context.Vacancy.Remove(vacancy);
            _context.SaveChanges();
        }
    }
}
