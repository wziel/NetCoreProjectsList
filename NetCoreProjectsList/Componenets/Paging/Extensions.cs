using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreProjectsList.Componenets.Paging
{
    public static class Extensions
    {
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> query, PagingInfo pagingInfo)
        {
            var totalCount = query.Count();
            pagingInfo.ItemsTotalCount = totalCount;
            if(pagingInfo.ItemsToSkipCount > 0)
            {
                query = query.Skip(pagingInfo.ItemsToSkipCount);
            }
            var list = query.Take(pagingInfo.ItemsPerPage).ToList();
            return new PagedList<T>
            {
                PagingInfo = pagingInfo,
                Page = list,
            };
        }
    }
}
