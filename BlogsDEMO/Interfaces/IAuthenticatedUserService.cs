using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsDEMO.Application.Interfaces
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
        string RoleId { get; }
        string RoleName { get; }
    }
}
