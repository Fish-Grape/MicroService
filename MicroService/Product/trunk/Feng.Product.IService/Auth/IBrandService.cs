﻿using Feng.Basic;
using Feng.Product.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Feng.Product.IService
{
    public interface IBrandService
    {
        ReturnResult<dynamic> AddBrand(string platKey,AddBrandModel model);

        ReturnResult ModifyBrand(string platKey, BrandModel model);

        ReturnResult ModifyBrandStatus(string platKey, int id, bool status);
        ReturnResult<PageList<BrandModel>> QueryBrand(string platKey,int pageIndex,int pageSize, string name);
    }
}
