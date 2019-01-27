using Feng.Files.Entity;
using Feng.Files.IService;
using Feng.Files.Model;
using Feng.Mongodb;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Feng.Files.Service
{
    public class FileService : IFileService
    {
        public readonly IDomainService _domainService;
        public readonly IMongoHelper<domain_file> _dbHelper;
        private readonly string col = "domain_file";

        public FileService(IDomainService domainService, IMongoHelper<domain_file> dbHelper)
        {
            _domainService = domainService;
            _dbHelper = dbHelper;
        }


        public FileModel FindFile(string appid,string fileid) {

            //验证归属方状态，买坑

            //查找文件
            FileModel file = new FileModel();

            var item = _dbHelper.FindOne(col, fileid);
            file.stream = _dbHelper.OpenDownToStreamByObjectId(ObjectId.Parse(item.fileid.ToString()));

            file.contentType = ContentTypeDic.GetContentType(item.ext.ToLower());

            return file;
                
        }

    }
}
