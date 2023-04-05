using AngleSharp;
using AngleSharp.Dom;
using ParserWorksSites.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace ParserWorksSites.Parsers
{
    public class WorkUaParser
    {
        public static async Task<IEnumerable<Vacancy>> GetObjectsFromDivs(IEnumerable<IElement> divs, string parentLink)
        {
            IEnumerable<Vacancy> vacancy = Enumerable.Empty<Vacancy>();
            foreach (var div in divs)
            {
                if (div != null)
                {

                    var vacancyUrl = String.Concat(parentLink, div.QuerySelector("h2")?.QuerySelector("a")?.GetAttribute("href"));
                    var config = Configuration.Default.WithDefaultLoader();
                    var context = BrowsingContext.New(config);
                    var vacancyDocument = await context.OpenAsync(vacancyUrl);
                    var categoryElement = vacancyDocument.QuerySelector("span.add-top-xs");
                    var type = categoryElement?.TextContent;
                    context.Dispose();

                    vacancy = vacancy.Append(new Vacancy(
                div.QuerySelector("h2")?.TextContent,
                type,
                String.Concat(parentLink, div.QuerySelector("h2")?.QuerySelector("a")?.GetAttribute("href"))
            ));
                }
            }
            return vacancy;
        }

        public static async Task<int> GetTotalPages(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);

            var pagination = document.QuerySelector("ul.pagination.hidden-xs");

            if (pagination != null)
            {
                var pages = pagination.QuerySelectorAll("li");

                int totalPages = (pages.Length < 2) ? 1 : int.Parse(pages[pages.Length - 2].TextContent);
                return totalPages;
            }
            else
            {
                throw new Exception("can`t find the amount of pages: DataParcer.GetTotalPages()");
            }
        }
    }
}