using Feng.Basic;
using Feng.Files.Model;
using System;

namespace Feng.Files.IService
{
    public interface IDomainService
    {
        ReturnResult<ReturnOpenModel> OpenDomain(OpenModel model);

        long ValidSize(string id);

        int ModifyDomain(string id, DomainModel model);
    }
}
