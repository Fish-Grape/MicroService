using Feng.Admin.Entity;
using Feng.Admin.IService;
using Feng.Admin.Model;
using Feng.Basic;
using Feng.Core;
using Feng.Redis;
using SqlSugar;
using System;
using System.Threading.Tasks;

namespace Feng.Admin.Service
{
    public class ProjectService : IProjectService
    {
        private readonly SqlSugarClient _dbContext;
        private readonly RedisClient _redisClient;
        private readonly IJsonHelper _jsonHelper;
        private readonly IUser _user;
        public ProjectService(SqlSugarClient dbContext,
            RedisClient redisClient,
            IJsonHelper jsonHelper,
            IUser user)
        {
            _dbContext = dbContext;
            _redisClient = redisClient;
            _jsonHelper = jsonHelper;
            _user = user;
        }

        public async  Task<ReturnResult<dynamic>> QueryProject()
        {
            var list = await _dbContext.Queryable<ProjectInfo>()
                                 .ToListAsync();
            return new ReturnResult<dynamic>(list);
        }

        public async Task<ReturnResult> SetProject(ProjectModel model)
        {
            try {
                var item = new ProjectInfo()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Key = model.Key,
                    Remark = model.Remark,
                    RouteKey = model.RouteKey
                };
                if (item.Id > 0)
                {
                    await _dbContext.Updateable(item)
                        .ExecuteCommandAsync();
                }
                else
                {
                    await _dbContext.Insertable(item).ExecuteCommandAsync();
                }
            }
            catch (Exception ex) {
                throw new Exception("项目设置失败！", ex);
            }
            return new ReturnResult();
        }
    }
}
