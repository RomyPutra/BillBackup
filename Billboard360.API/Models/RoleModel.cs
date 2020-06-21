using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billboard360.API.Models
{
    public class IsRoleInputModel 
    {
        public bool IsSupervisor { get; set; }
    }

    public class RoleOutputModel
    {
        public Guid RoleID { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
    }

    public class RoleResponseModel: BaseResponseModel
    {
        public List<RoleOutputModel> data { get; set; }
    }
}
