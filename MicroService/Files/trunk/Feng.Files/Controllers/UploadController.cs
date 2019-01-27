using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Feng.Basic;
using Feng.Files.IService;
using Feng.Files.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;

namespace Feng.Files.Controllers
{
    /// <summary>
    /// 文件上传
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService _uploadService;
        public UploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        /// <summary>
        /// 表单模式上传文件
        /// </summary>
        /// <param name="appid">编号</param>
        /// <returns></returns>
        [HttpPost,Route("{appid}")]
        public async Task<ReturnResult<UploadReturnModel>> UploadSmallFilesByForm(string appid) {

            var files = Request.Form.Files;
            
            return await _uploadService.UploadSmallFile(appid, files);
        }

    }
}