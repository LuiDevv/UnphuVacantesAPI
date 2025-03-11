using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;

namespace api.interfaces
{
    public interface IRoleInterface
    {
         Task<IEnumerable<RoleDTO>> GetAllRoles();
        Task<RoleDTO> GetRoleById(int id);
        Task<RoleDTO> CreateRole(CreateRoleRequest request);
        Task<bool> UpdateRole(int id, UpdateRoleRequest request);
        Task<bool> DeleteRole(int id);
    }
}