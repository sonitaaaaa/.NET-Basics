using BlogsDEMO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsDEMO.Domain.Entities
{
    public class Permission : AuditableBaseEntity
    {
        public string FunctionName { get; set; }
        public string CanAccess { get; set; }
        public string CanAdd { get; set; }
        public string CanDelete { get; set; }
        public string Url { get; set; }

    }
}
