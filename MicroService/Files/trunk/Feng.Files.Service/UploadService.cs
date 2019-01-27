using Feng.Basic;
using Feng.Files.Entity;
using Feng.Files.IService;
using Feng.Files.Model;
using Feng.Mongodb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feng.Files.Service
{
    public class UploadService : IUploadService
    {

        public readonly IMongoHelper<domain_file> _dbHelper;
        private readonly string col = "domain_file";
        public readonly IDomainService _domainService;

        public UploadService(IDomainService domainService, IMongoHelper<domain_file> dbHelper)
        {
            _domainService = domainService;
            _dbHelper = dbHelper;
        }

        /// <summary>
        /// 上传文件服务
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public async Task<ReturnResult<UploadReturnModel>> UploadSmallFile(string appid, IFormFileCollection files) {
            
            long validSize = _domainService.ValidSize(appid);

            if (validSize <= 0)
                return new ReturnResult<UploadReturnModel>((int)ErrorCodeEnum.Failed, null, "未开启文件服务或空间不足！");
            
            if (files.Count <= 0)
            {
                return new ReturnResult<UploadReturnModel>((int)ErrorCodeEnum.Error_NoData, null, "没有接收到数据!");
            }

            long uploadSize = files.Sum(f => f.Length);

            if (validSize < uploadSize)
                return new ReturnResult<UploadReturnModel>((int)ErrorCodeEnum.Failed, null, "文件空间已满,请升级空间!");

            

            UploadReturnModel reItem = new UploadReturnModel();
            reItem.files = new List<UploadFiles>();
            foreach (IFormFile item in (FormFileCollection)files)
            {
                string name = item.FileName;
                
                string fileid = _dbHelper.UploadByStream(item.FileName, item.OpenReadStream());

                UploadFiles reModel = null;
                if (string.IsNullOrWhiteSpace(fileid)) {
                    reModel = new UploadFiles()
                    {
                        id = "",
                        key = item.Name
                    };
                    reItem.files.Add(reModel);
                    continue;
                }
                domain_file df = new domain_file();
                df.id = ObjectId.GenerateNewId();
                df.domainid = ObjectId.Parse(appid);
                df.addtime = DateTime.Now;
                df.visit = 0;
                df.fileid = ObjectId.Parse(fileid);
                df.name = item.FileName;
                df.ext = item.FileName.Substring(item.FileName.LastIndexOf('.')+1);
                df.cat = FileTypeEnum.GetFileTypeByV(df.ext.ToLower());
                _dbHelper.AddOne(col, df);

                reModel = new UploadFiles()
                {
                    id = df.id.ToString(),
                    key = item.Name
                };
                reItem.files.Add(reModel);
            }

            
            return new ReturnResult<UploadReturnModel>(reItem);
        }


    }
}
