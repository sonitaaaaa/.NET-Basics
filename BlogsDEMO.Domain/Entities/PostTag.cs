using BlogsDEMO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsDEMO.Domain.Entities
{
    public class PostTag : AuditableBaseEntity
    {
        public string PostId { get; set; }
        public string TagId { get; set; }
    }
}
