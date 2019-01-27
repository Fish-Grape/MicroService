using System.Threading.Tasks;
using Feng.Admin.Model;
using Feng.Basic;

namespace Feng.Admin.IService
{
    public interface IConfigService
    {
        Task<ReturnResult<dynamic>> QueryAppList();
        Task<ReturnResult> SetAppInfo(AppModel model);

        Task<ReturnResult<PageList<dynamic>>> QueryAppProjectList(QueryAppProjectModel model);

        Task<ReturnResult> SetAppProjectInfo(AppProjectModel model);

        Task<ReturnResult<PageList<dynamic>>> QueryAppConfigList(QueryAppConfigModel model);

        Task<ReturnResult> SetAppConfigInfo(AppConfigModel model);
    }
}
