
using Feng.Basic;
using Feng.Files.IService;
using Feng.Files.Model;
using Microsoft.AspNetCore.Mvc;

namespace Feng.Files.Controllers
{
    /// <summary>
    /// 归属方
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DomainController : ControllerBase
    {
        private readonly IDomainService _domainService;
        public DomainController(IDomainService domainService) {
            _domainService = domainService;
        }

        /// <summary>
        /// 开通文件系统
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost,Route("open")]
        public ReturnResult<ReturnOpenModel> OpenDomain([FromBody]OpenModel model) {
            if (model == null)
                return new ReturnResult<ReturnOpenModel>((int)ErrorCodeEnum.Parameter_Missing, null, "参数异常!");

            return _domainService.OpenDomain(model);
            
        }


    }
}