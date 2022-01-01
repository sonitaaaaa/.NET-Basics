using BlogsDEMO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsDEMO.Domain.Entities
{
    public class Role : AuditableBaseEntity
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
    }
}
