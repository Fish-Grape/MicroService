using Feng.Admin.Model;
using Feng.Basic;
using System.Threading.Tasks;

namespace Feng.Admin.IService
{
    public interface IRoleService
    {
        /// <summary>
        /// 查询所有角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ReturnResult<dynamic>> QueryAllRoles(QueryRolesByPlatformModel model);
        /// <summary>
        /// 查询可用角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ReturnResult<dynamic>> QueryRoles(QueryRolesByPlatformModel model);

        /// <summary>
        /// 查询角色权限信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ReturnResult<dynamic>> QueryRoleInfo(QueryRolesByIdModel model);

        /// <summary>
        /// 设置角色信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ReturnResult> SetRole(ModifyRolesModel model);
    }
}
