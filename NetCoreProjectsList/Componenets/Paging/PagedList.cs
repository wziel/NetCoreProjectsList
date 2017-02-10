using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreProjectsList.Componenets.Paging
{
    public class PagedList<T>
    {
        public PagingInfo PagingInfo { get; set; }
        public List<T> Page { get; set; }
    }
}
