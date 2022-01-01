using BlogsDEMO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsDEMO.Domain.Entities
{
    public class Tag : AuditableBaseEntity
    {
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Slug { get; set; }
    }
}
