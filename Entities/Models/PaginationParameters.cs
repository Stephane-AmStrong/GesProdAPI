using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class PaginationParameters
    {
        //const int maxPageSize = 250;
        const int maxPageSize = 2500;
        public int PageNumber { get; set; } = 1;
        //private int _pageSize = 10;
        private int _pageSize = 1000;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
