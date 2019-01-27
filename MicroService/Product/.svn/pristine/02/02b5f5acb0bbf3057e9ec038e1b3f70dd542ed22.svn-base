using Feng.Basic;
using Feng.DbContext;
using Feng.Product.Entity;
using Feng.Product.IService;
using Feng.Product.Model;
using Feng.Utils.Helpers;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Feng.Product.Service
{
    public class CategoryService : ICategoryService
    {
        public readonly IRepositoryBase<category> _repositoryBase;
        public CategoryService(IRepositoryBase<category> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }
        /// <summary>
        /// 添加分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<dynamic> AddCategory(string platKey, AddCategoryModel model)
        {
            if (model == null)
                return new ReturnResult<dynamic>((int)ErrorCodeEnum.Parameter_Missing, null, "参数异常!");

            category item = new category()
            {
                id = Id.ObjectId(),
                name = model.categoryName,
                platkey = platKey,
                pid = model.parentId.Trim(),
                icon = model.categoryIcon,
                order = model.displayOrder
            };

            bool result = _repositoryBase.Insert(item);
            if (!result)
                return new ReturnResult<dynamic>((int)ErrorCodeEnum.Error_SqlData, null, "添加失败!");

            return new ReturnResult<dynamic>(new { item.id });

        }

        public ReturnResult ModifyCategory(string platKey, CategoryModel model)
        {
            if (model == null)
                return new ReturnResult((int)ErrorCodeEnum.Parameter_Missing, "参数异常!");

            //category item = new category()
            //{
            //     id=model.categoryId,
            //     platkey= platKey,
            //    name = model.categoryName,
            //    icon = model.categoryIcon,
            //    order = model.displayOrder
            //};

            bool result = _repositoryBase.DbContext.Updateable<category>()
                .UpdateColumns(x => new category { name=model.categoryName, icon=model.categoryIcon, order=model.displayOrder })
                .Where(x => x.platkey == platKey)
                .Where(x => x.id == model.categoryId)
                .ExecuteCommand() > 0;

            if (!result)
                return new ReturnResult((int)ErrorCodeEnum.Error_SqlData, "修改失败!");

            return new ReturnResult();
        }

        public ReturnResult<List<ReturnListCategoryModel>> QueryCategory(string platKey)
        {
            var list =  _repositoryBase.DbContext
                .Queryable<category>()
                .Where(x => x.platkey == platKey)
                .OrderBy(x => x.order, OrderByType.Asc)
                .ToList();

            List<ReturnListCategoryModel> result = ReList("", list);

            return new ReturnResult<List<ReturnListCategoryModel>>(result);
        }

        private List<ReturnListCategoryModel> ReList(string pid, List<category> list)
        {
            var child = list.FindAll(x => x.pid == pid);
            if (child.Count <= 0)
                return null;
            List<ReturnListCategoryModel> result = new List<ReturnListCategoryModel>();

            child.ForEach(x =>
            {
                ReturnListCategoryModel model = new ReturnListCategoryModel()
                {
                    value = x.id,
                    label = x.name,
                    categoryIcon = x.icon,
                    children = ReList(x.id, list)
                };
                result.Add(model);
            });
            return result;
        }

        public ReturnResult RemoveCategory(string platKey, string cid)
        {
            bool existChild = _repositoryBase.Count(x => x.pid == cid && x.platkey == platKey) > 0;

            bool existGroup = _repositoryBase.DbContext.Queryable<attribute_group>()
                .Where(x => x.platkey == platKey)
                .Where(x => x.categoryid == cid)
                .Count() > 0;

            if (existChild || existGroup)
                return new ReturnResult("该分类存在子项或属性分组,请先删除子项!");

            bool result = _repositoryBase.Delete(x => x.id == cid && x.platkey== platKey);

            if (!result)
                return new ReturnResult((int)ErrorCodeEnum.Error_SqlData, "删除失败!");

            return new ReturnResult();
        }
    }
}
