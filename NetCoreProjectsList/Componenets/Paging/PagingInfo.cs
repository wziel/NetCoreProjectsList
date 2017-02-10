using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreProjectsList.Componenets.Paging
{
    public class PagingInfo
    {
        private int currentPage = 1;
        private int itemsPerPage = 0;
        private int itemsTotalCount = 0;

        public int CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = (value <= 0 ? 1 : value);
            }
        }
        
        public int ItemsPerPage
        {
            get { return itemsPerPage; }
            set
            {
                itemsPerPage = (value < 0 ? 0 : value);
            }
        }

        public int ItemsToSkipCount
        {
            get
            {
                return (CurrentPage - 1) * ItemsPerPage;
            }
        }

        public int ItemsTotalCount
        {
            get { return ItemsTotalCount; }
            set
            {
                itemsTotalCount = (value < 0 ? 0 : value);
                if (ItemsToSkipCount >= itemsTotalCount)
                {
                    CurrentPage = LastPage;
                }
            }
        }

        public bool IsFirstPage
        {
            get
            {
                return CurrentPage == 1;
            }
        }

        public bool IsLastPage
        {
            get
            {
                return currentPage == LastPage;
            }
        }

        public int LastPage
        {
            get
            {
                return (int)Math.Ceiling(((double)itemsTotalCount / itemsPerPage));
            }
        }

        public int FirstPage
        {
            get
            {
                return 1;
            }
        }
    }
}
