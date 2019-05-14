using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace tsv_core.Models
{
    public class EFCDatabaseRequestLogger
    {
        LoggingDbContext context;

        public EFCDatabaseRequestLogger(LoggingDbContext ctx)
        {
            context = ctx;
        }

        public async Task<List<Request>> GetRequestRepository()
        {
            return await context.Requests.Where(r => !r.IPAddressClient.Equals("127.0.0.1") 
            && !r.IPAddressClient.Equals("::1") 
            && DateTime.Parse(r.Time, CultureInfo.InvariantCulture) >= DateTime.Today.AddDays(-7)).ToListAsync();
        }
        /// <summary>
        /// Gets all Requests from the Databse as a List<Request> in reversed order.
        /// </summary>
        /// <returns>List<Request></returns>
        public async Task<List<Request>> GetRequestRepositoryReverse()
        {
            var repo = await GetRequestRepository();
            return repo.Reverse<Request>().ToList();
        }

        public async Task<List<Request>> GetRequestsInSpecifiedTimeSpan(DateTime beginning, DateTime end)
        {
            var repoReverse = await GetRequestRepositoryReverse();
            return repoReverse.Where(r => DateTime.Parse(r.Time, CultureInfo.InvariantCulture) >= beginning && DateTime.Parse(r.Time, CultureInfo.InvariantCulture) <= end).ToList();
        }

        public List<RequestPathWithCount> GetDistinctRequestPathsWithCounts(List<Request> list)
        {
            List<string> pathList = new List<string>();
            foreach(Request r in list)
            {
                pathList.Add(r.Path);
            }

            List<string> noDuplicate = pathList.Distinct().ToList();

            List<RequestPathWithCount> result = new List<RequestPathWithCount>();
            foreach(string path in noDuplicate)
            {
                result.Add(new RequestPathWithCount { Path = path, Count = pathList.Where(p => p == path).Count() });
            }

            return result.OrderByDescending(r => r.Count).ToList();
        }

        public List<RequestPathWithCount> GetCleanDistinctRequestPathsWithCounts(List<Request> list)
        {
            return GetDistinctRequestPathsWithCounts(list).Where(r => ContainsControllerName(r.Path) || r.Path.Equals("/")).ToList();
        }

        public async Task LogRequest(Request request)
        {
            await context.Requests.AddAsync(request);
            await context.SaveChangesAsync();
        }

        
        public async Task DeleteAll()
        {
            foreach (var elem in context.Requests)
            {
                context.Requests.Remove(elem);
            }
            await context.SaveChangesAsync();
        }


        private bool ContainsControllerName(string path)
        {
            string s = path.ToLower();
            return s.Contains("/account") || s.Contains("/admin") || s.Contains("/home") || s.Contains("/roleadmin") || s.Contains("/userarea");
        }
    }

    public class RequestPathWithCount
    {
        public string Path {get; set;}
        public int Count { get; set; }
    }
}
