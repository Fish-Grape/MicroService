using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Feng.Admin.IService;
using Feng.Admin.Model;
using Feng.Basic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Feng.Admin.Controllers
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    [Produces("application/json")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleService"></param>
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// 查询所有角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize("permission")]
        [HttpGet("/Role/QueryAllRoles")]
        public async Task<ReturnResult<dynamic>> QueryAllRoles([FromQuery]QueryRolesByPlatformModel model)
        {
            return await _roleService.QueryAllRoles(model);
        }

        /// <summary>
        /// 查询可用角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize("permission")]
        [HttpGet("/Role/QueryRoles")]
        public async Task<ReturnResult<dynamic>> QueryRoles([FromQuery]QueryRolesByPlatformModel model)
        {
            return await _roleService.QueryRoles(model);
        }

        /// <summary>
        /// 查询角色权限信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize("permission")]
        [HttpGet("/Role/QueryRoleInfo")]
        public async Task<ReturnResult<dynamic>> QueryRoleInfo([FromQuery]QueryRolesByIdModel model)
        {
            return await _roleService.QueryRoleInfo(model);
        }
        /// <summary>
        /// 设置角色信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize("permission")]
        [HttpPost("/Role/SetRole")]
        public async Task<ReturnResult> SetRole([FromBody]ModifyRolesModel model)
        {
            return await _roleService.SetRole(model);
        }
    }
}