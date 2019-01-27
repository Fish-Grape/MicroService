using System.Threading.Tasks;
using Feng.Admin.IService;
using Feng.Admin.Model;
using Feng.Basic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Feng.Admin.Controllers
{
    /// <summary>
    /// 项目控制器
    /// </summary>
    [Produces("application/json")]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        /// <summary>
        /// 查看项目列表信息
        /// </summary>
        /// <returns></returns>
        [Authorize("permission")]
        [HttpGet("/Project/QueryProject")]
        public async Task<ReturnResult<dynamic>> QueryProject()
        {
            return await _projectService.QueryProject();
        }

        /// <summary>
        /// 设置项目信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize("permission")]
        [HttpPost("/Project/SetProject")]
        public async Task<ReturnResult> SetProject([FromBody]ProjectModel model)
        {
            return await _projectService.SetProject(model);
        }
    }
}