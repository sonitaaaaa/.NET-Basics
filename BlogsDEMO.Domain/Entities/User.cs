using BlogsDEMO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsDEMO.Domain.Entities
{
    public class User : AuditableBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public short Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AvatarUrl { get; set; }
        public string RoleId { get; set; }
        public string UpdatedBy { get; set; }
        public bool Active { get; set; }

    }
}
