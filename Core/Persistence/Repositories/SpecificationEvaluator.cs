using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    //this class used to concat my query(context.DbSet<>.(Where().Include)=>specifications class
    public static class SpecificationEvaluator
    {
        public static IQueryable<T> GetQuery<T>(IQueryable<T> inputQuery,Specifications<T> specifications )where T : class
        {
            //inputquery =>Dbset<>
            var query = inputQuery;
            if(specifications.Criteria is not null)
                query = query.Where(specifications.Criteria);

           
            //foreach(var specification in specifications.IncludeExpression)
            //{
            //    query = query.Include(specification);
            //}

            query = specifications.IncludeExpression.Aggregate(query, (currentQuery, includeExpression) =>
            currentQuery.Include(includeExpression));


            if (specifications.OrderBy is not null)
                query=query.OrderBy(specifications.OrderBy);

           else if (specifications.OrderByDescending is not null)
                query = query.OrderByDescending(specifications.OrderByDescending);

           if(specifications.IsPaginated)
                query = query.Skip(specifications.Skip).Take(specifications.Take);

            return query;
        }
    }
}
