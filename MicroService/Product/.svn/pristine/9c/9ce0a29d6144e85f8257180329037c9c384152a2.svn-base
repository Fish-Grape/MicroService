using Feng.Basic;
using Feng.Product.Model;
using System.Collections.Generic;
using Feng.Product.Model.Background;

namespace Feng.Product.IService
{
    public interface IAttributeService
    {

        #region 属性分组
        ReturnResult<dynamic> AddAttributeGroup(string platKey, AddAttributeGroupModel model);

        ReturnResult<List<AttributeGroupModel>> QueryAttributeGroup(string platKey,string categoryId);

        ReturnResult RemoveAttributeGroup(string platKey,string id);

        ReturnResult ModifyAttributeGroup(string platKey, AttributeGroupModel model);

        #endregion

        #region 属性
        ReturnResult<dynamic> AddAttribute(string platKey, AddAttributeModel model);

        ReturnResult<List<AttributeModel>> QueryAttribute(string platKey, string categoryId);

        ReturnResult RemoveAttribute(string platKey, string id);

        ReturnResult ModifyAttribute(string platKey, AttributeModel model);

        #endregion

        #region 属性值
        ReturnResult<dynamic> AddAttributeValue(string platKey, AddAttributeValueModel model);

        ReturnResult<List<AttributeValueModel>> QueryAttributeValue(string platKey, string attributeId);

        ReturnResult RemoveAttributeValue(string platKey, string id);

        ReturnResult ModifyAttributeValue(string platKey, AttributeValueModel model);

        #endregion

        #region 属性/属性值
        ReturnResult<List<ProductAttrModel>> QueryAttributeAndVal(string platKey, string categoryId);
        #endregion

        #region 附加属性
        ReturnResult<dynamic> AddExtendAttribute(string platKey, AddExtendAttrModel model);

        ReturnResult<List<ExtendAttrModel>> QueryExtendAttr(string platKey, string ex_id);

        ReturnResult RemoveExtendAttr(string platKey, string id);

        ReturnResult ModifyExtendAttr(string platKey, ExtendAttrModel model);
        #endregion
    }
}
