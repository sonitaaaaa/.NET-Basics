using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsDEMO.Application.Parameters
{
    public class RequestParameter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public string Order { get; set; }
        public bool CountFlag { get; set; } = true;
        public bool IsExport { get; set; }

        public RequestParameter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
            this.SortBy = string.Empty;
            this.Order = "asc";
            this.CountFlag = true;
            this.IsExport = false;
        }

        public RequestParameter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
