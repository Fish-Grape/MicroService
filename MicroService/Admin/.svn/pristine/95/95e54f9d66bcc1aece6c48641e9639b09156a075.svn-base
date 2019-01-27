using Feng.Admin.Model;
using Feng.Basic;
using System.Threading.Tasks;

namespace Feng.Admin.IService
{
    public interface IApiService
    {
        /// <summary>
        /// 查询Api资源
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ReturnResult<PageList<dynamic>>> QueryApis(QueryApiModel model);
        /// <summary>
        /// 设置Api资源
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ReturnResult> SetApi(ApiModel model);
    }
}
