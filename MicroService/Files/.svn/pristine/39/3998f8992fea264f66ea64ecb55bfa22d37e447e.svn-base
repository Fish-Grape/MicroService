using Feng.Basic;
using Feng.Files.Entity;
using Feng.Files.IService;
using Feng.Files.Model;
using Feng.Mongodb;
using MongoDB.Bson;
using System;

namespace Feng.Files.Service
{
    public class DomainService : IDomainService
    {
        public readonly IMongoHelper<domain> _dbHelper;
        public readonly string col = "domain";

        public DomainService(IMongoHelper<domain> dbHelper) {
            _dbHelper = dbHelper;
        }

        public ReturnResult<ReturnOpenModel> OpenDomain(OpenModel model)
        {
            ReturnOpenModel item = null;
            domain oInfo = new domain()
            {
                id = ObjectId.GenerateNewId(),
                bucketcount = model.bucketCount,
                bucketsize = model.bucketSize,
                existbucket = 0,
                existsize = 0,
                existcount = 0,
                status = model.status,
                addtime = DateTime.Now
            };

            int result= _dbHelper.AddOne(col, oInfo);
            if (result > 0) {
                item = new ReturnOpenModel();
                item.AppId = oInfo.id.ToString();
                item.bucketSize = oInfo.bucketsize;
                item.bucketCount = oInfo.bucketcount;
                item.status = oInfo.status;
            }
            return new ReturnResult<ReturnOpenModel>(item);
        }
        /// <summary>
        /// 有效空间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public long ValidSize(string id) {

            string[] filed = new string[] { "bucketsize", "existsize","status" };
            var item = _dbHelper.FindOne(col, id, filed);
            if (item != null)
            {
                if (item.status == 0)
                    return 0;

                return item.bucketsize - item.existsize;
            }
            else
                return 0;
        }


        public int ModifyDomain(string id, DomainModel model) {
            try
            {
                domain item = new domain()
                {

                };
                var result = _dbHelper.ModifyOne(col, item, id);
                if (result.ModifiedCount > 0)
                    return 1;
                else
                    return 0;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

    }
}
