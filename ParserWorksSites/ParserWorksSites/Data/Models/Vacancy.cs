using System.ComponentModel.DataAnnotations;

namespace ParserWorksSites.Data.Models
{
    public class Vacancy
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Link { get; set; }

        public Vacancy() { }

        public Vacancy(string title, string type, string link)
        {
            Title = title;
            Type = type;
            Link = link;
        }
    }
}