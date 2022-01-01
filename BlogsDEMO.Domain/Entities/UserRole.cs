using BlogsDEMO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsDEMO.Domain.Entities
{
    public class UserRole : AuditableBaseEntity
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}
