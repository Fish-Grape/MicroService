using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Feng.Files.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Feng.Files.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="fileid"></param>
        /// <returns></returns>
        [HttpGet, Route("{appid}/{fileid}")]
        public  IActionResult ViewOrDownLoadFileById(string appid,string fileid) {

            var item = _fileService.FindFile(appid, fileid);

            return File(item.stream, item.contentType);
        }
    }
}